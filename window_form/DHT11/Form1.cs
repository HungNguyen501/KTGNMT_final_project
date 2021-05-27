using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using ZedGraph;
using Npgsql;

namespace DHT11
{
    public partial class Form1 : Form
    {
        SerialPort serialPort = null;
        NpgsqlConnection postgres_connection = null;

        DataInfo newDataInfor = new DataInfo();
        DataInfo cacheDataInfor = new DataInfo();

        GraphPane humidityPanel;
        GraphPane temperaturePanel;

        const int LED_ON = 1;
        const int LED_OFF = 0;
        int LED = 0;
        int last_state_LED = 0;

        List<DataInfo> listData = new List<DataInfo>() { };
        RollingPointPairList listHumidPoint = new RollingPointPairList(10000);
        RollingPointPairList listTemPoint = new RollingPointPairList(10000);        
   
        double timeSeconds = 0;

        public Form1()
        {
            InitializeComponent();

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                var cs = "Host=localhost; Port=5432; Username=postgres;Password=123456;Database=postgres";
                postgres_connection = new NpgsqlConnection(cs);
                postgres_connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                serialPort = new SerialPort();
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                serialPort.PortName = "COM10";
                serialPort.BaudRate = 9600;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.Parity = Parity.None;

                serialPort.Open();

            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            


            humidityPanel = zedGraphControl.GraphPane;
            humidityPanel.Title.Text = "Humidity";
            humidityPanel.XAxis.Title.Text = "time(s)";
            humidityPanel.YAxis.Title.Text = "Percent (%)";

            temperaturePanel = zedGraphControl1.GraphPane; 
            temperaturePanel.Title.Text = "Temperature";
            temperaturePanel.XAxis.Title.Text = "time(s)";
            temperaturePanel.YAxis.Title.Text = "Celsius (°C)";
            
        }

        //Recieve data from sensors
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int[] data = new int[4];

            for (int i = 0; i < 4; i++)
            {
                data[i] = sp.ReadByte();
                //Console.WriteLine(data[i]);
            }

            // tem = I_H + D_H/10
            newDataInfor.humidityPoint = data[0] + data[1] / 10;

            // tem = I_Temp + D_temp/10
            newDataInfor.temperaturePoint = data[2] + data[3]/10;

            adddData();
            
        }

        private void adddData()
        {
            timeSeconds = timeSeconds + 0.2;
            setRealTimeChart();
  
            UpdateTextBox("");
            newDataInfor.timeReceived = DateTime.Now.TimeOfDay.ToString().Substring(0, 10);
            listData.Add(newDataInfor);

            cacheDataInfor = newDataInfor;
            newDataInfor = new DataInfo();
        }

        public void UpdateTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateTextBox), new object[] { value });
                return;
            }

            temp_value_lable.Text = newDataInfor.temperaturePoint.ToString(); ;
            humid_value_lable.Text = newDataInfor.humidityPoint.ToString();

            //Control LED
            var color = Color.Green;
            string text = "normal";

            if (newDataInfor.humidityPoint <= 45 &&
                newDataInfor.humidityPoint >= 40 &&
                newDataInfor.temperaturePoint <= 26 &&
                newDataInfor.temperaturePoint >= 20)
            {
                LED = LED_OFF;
            }
            else
            {
                LED = LED_ON;
                color = Color.Red;
                text = "warning";
            }

            if (last_state_LED != LED)
            {
                status_value_lable.Text = text;
                status_value_lable.ForeColor = color;
                serialPort.Write(new char[] { '0', '1' }, LED, 1);
            }

            last_state_LED = LED;
            newDataInfor.LED = LED;

        }

        private void setChart(ZedGraphControl zedGraphControl, GraphPane graphPane, RollingPointPairList listPoint, double temperature)
        {
            zedGraphControl.GraphPane.CurveList.Clear();
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
            listPoint.Add(timeSeconds, temperature);

            graphPane.AddCurve("Point ", listPoint, Color.Red, SymbolType.None);
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private void setRealTimeChart()
        {
            setChart(zedGraphControl, humidityPanel, listHumidPoint, newDataInfor.humidityPoint);
            setChart(zedGraphControl1, temperaturePanel, listTemPoint, newDataInfor.temperaturePoint);
                             
        }     

        private void sendDataButton_Click(object sender, EventArgs e)
        {
            if (postgres_connection != null && postgres_connection.State == ConnectionState.Open)
            {
                try
                {
                    using (var cmd = new NpgsqlCommand())
                    {
                        

                        cmd.Connection = postgres_connection;
                        cmd.CommandText = "INSERT INTO sensor_record(time, temperature, humidity, led)" + 
                                           " VALUES('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                                            "','" + cacheDataInfor.temperaturePoint +
                                            "','" + cacheDataInfor.humidityPoint +
                                            "'," + cacheDataInfor.LED +
                                            ");";
                        Console.WriteLine(cmd.CommandText);
                        cmd.ExecuteNonQuery();
                    }
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
                    
            }
        }

        private void zedGraphControl_Load(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void humid_lable_Click(object sender, EventArgs e)
        {

        }

        private void humid_value_lable_Click(object sender, EventArgs e)
        {

        }

        private void temp_value_lable_Click(object sender, EventArgs e)
        {

        }

        private void temp_lable_Click(object sender, EventArgs e)
        {

        }

        private void status_lable_Click(object sender, EventArgs e)
        {

        }

        private void status_value_lable_Click(object sender, EventArgs e)
        {

        }

    }
    public class DataInfo
    {
        public double temperaturePoint { get; set; }
        public double humidityPoint { get; set; }
        public int LED { get; set; }

        public string timeReceived { get; set; }

        public DataInfo()
        {

        }
    }
}
