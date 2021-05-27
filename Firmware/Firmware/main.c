#define F_CPU 8000000UL			/* Define frequency here its 8MHz */
#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>
#include <stdlib.h>
#include <stdio.h>

#define DHT11_PIN 6

//#define USART_BAUDRATE 9600
#define BAUD_PRESCALE (((F_CPU / (USART_BAUDRATE * 16UL))) - 1)

uint8_t data = 0;
uint8_t I_RH;
uint8_t D_RH;
uint8_t I_Temp;
uint8_t D_Temp;
uint8_t CheckSum;

void Request()				/* Microcontroller send start pulse/request */
{
	DDRD |= (1<<DHT11_PIN);
	PORTD &= ~(1<<DHT11_PIN);	/* set to low pin */
	_delay_ms(20);			/* wait for 20ms */
	PORTD |= (1<<DHT11_PIN);	/* set to high pin */
}

void Response()				/* receive response from DHT11 */
{
	DDRD &= ~(1<<DHT11_PIN);
	while(PIND & (1<<DHT11_PIN));
	while((PIND & (1<<DHT11_PIN))==0);
	while(PIND & (1<<DHT11_PIN));
}

uint8_t Receive_data()			/* receive data */
{
	for (int q=0; q<8; q++)
	{
		while((PIND & (1<<DHT11_PIN)) == 0);  /* check received bit 0 or 1 */
		_delay_us(30);

		/* if high pulse is greater than 30ms */
		if(PIND & (1<<DHT11_PIN)) {
			
			data = (data<<1)|(0x01);	/* then its logic HIGH */
		}
		/* otherwise its logic LOW */
		else {
			data = (data<<1);
		}
		
		while(PIND & (1<<DHT11_PIN));
	}

	return data;
}


void UART_init(long USART_BAUDRATE)
{
	UCSRB |= (1 << RXEN) | (1 << TXEN) | (1 << RXCIE);/* Turn on transmission and reception */
	UCSRC |= (1 << URSEL) | (1 << UCSZ0) | (1 << UCSZ1);/* Use 8-bit character sizes */
	UBRRL = BAUD_PRESCALE;		/* Load lower 8-bits of the baud rate value */
	UBRRH = (BAUD_PRESCALE >> 8);	/* Load upper 8-bits*/
}

unsigned char UART_RxChar()
{
	while ((UCSRA & (1 << RXC)) == 0);/* Wait till data is received */
	return(UDR);			/* Return the byte*/
}

void UART_TxChar(char ch)
{
	while (! (UCSRA & (1<<UDRE)));	/* Wait for empty transmit buffer*/
	UDR = ch ;
}

ISR(USART_RXC_vect)
{
	char ch = UART_RxChar();

	if(ch == '0')
	{
		PORTC = 0x00; // tat led
	}
	else if(ch == '1')
	{
		PORTC = 0x01; // bat led
	}
}

int main()
{
	
	UART_init(9600);
	DDRC = 0x01;
	sei();
	
	while(1)
	{
		Request();		/* send start pulse */
		Response();		/* receive response */

		I_RH=Receive_data();	/* store first eight bit in I_RH */
		D_RH=Receive_data();	/* store next eight bit in D_RH */
		I_Temp=Receive_data();	/* store next eight bit in I_Temp */
		D_Temp=Receive_data();	/* store next eight bit in D_Temp */
		CheckSum=Receive_data();/* store next eight bit in CheckSum */
		if ((I_RH + D_RH + I_Temp + D_Temp) == CheckSum)
		{
			UART_TxChar(I_RH);
			UART_TxChar(D_RH);
			UART_TxChar(I_Temp);
			UART_TxChar(D_Temp);
		}
		_delay_ms(100);
	}
}