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

namespace DHT11
{
    public partial class Form1 : Form
    {
        // Cấu hình FireBase
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "QcwJfwnQy0RFVCEFsaSzKveTGoloMNiD6ZbeG6E0",
            BasePath = "https://dht11-de5d1.firebaseio.com/"
        };
        IFirebaseClient client;
        SerialPort serialPort = null;
        int dem = 0;
        //Khai báo 2 biến nhận giá trị
        int number1;
        int number2;
        // Byte dữ liệu từ IC       
        int checkB = 4;  
        // Biến check dữ liệu là Temperature hay là Humidity
        int checkTorH = 0;
        DataInfo newDataInfor = new DataInfo();
        DataInfo cacheDataInfor = new DataInfo();
        GraphPane myPane;
        GraphPane myPane1;
        // Danh sách thông tin nhiệt độ (độ ẩm)
        List<DataInfo> lstTemperatureInfo = new List<DataInfo>() { };
        // Biểu đồ đường điểm đo độ ẩm
        RollingPointPairList listPoint1 = new RollingPointPairList(10000);
        // Biểu đồ đường điểm đo nhiệt độ
        RollingPointPairList listPoint2 = new RollingPointPairList(10000);        
        // Thời điểm đo
        double timeSeconds = 0;
       
        Boolean isStopView = false;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            serialPort = new SerialPort();
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            serialPort.PortName = "COM3";
            serialPort.BaudRate = 9600;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            client = new FireSharp.FirebaseClient(config);
            
            myPane = zedGraphControl.GraphPane;
            myPane.Title.Text = "Đo độ ẩm";
            myPane.XAxis.Title.Text = "Thời gian(s)";
            myPane.YAxis.Title.Text = "Độ ẩm";
            myPane1 = zedGraphControl1.GraphPane;
            myPane1.Title.Text = "Điểm Nhiệt độ";
            myPane1.XAxis.Title.Text = "Thời gian(s)";
            myPane1.YAxis.Title.Text = "Nhiệt độ";
            serialPort.Open();
        }
        //Nhận dữ liệu từ sensor 
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            //Console.WriteLine(1);
            SerialPort sp = (SerialPort)sender;
            int indata = sp.ReadByte();
            Console.WriteLine(indata);
            if(checkB == 4)
            {
                number1 = (int)indata;
                checkB--;
            }else if(checkB == 3)
            {
                number2 = (int)indata;
                newDataInfor.temperaturePoint = getPoint(1);
                adddData();
                checkB--;
            }else if(checkB == 2)
            {
                number1 =(int)indata;
                checkB--;
            }else if (checkB == 1)
            {
                number2 = (int)indata;
                newDataInfor.humidityPoint = getPoint(2);
                adddData();
                checkB = 4;
            }

            //if (checkB == 2)
            //{
            //    //temp.Add(indata);
            //    newDataInfor.temperaturePoint = (double)indata;
            //    adddData();
            //    checkB--;
            //}else if(checkB == 1)
            //{
            //    //tem.Add(indata);
            //    newDataInfor.humidityPoint = (double)indata;
            //    checkB = 2;
            //}
            //else if (checkB == 2)
            //{
            //    hum.Add(indata);
            //    newDataInfor.humidityPoint = indata;
            //    adddData();
            //    checkB--;
            //}
            //else if (checkB == 1)
            //{
            //    //hump.Add(indata);

            //    checkB = 4;
            //}
            
        }
        private void adddData()
        {
            if (!isStopView)
            {
                timeSeconds = timeSeconds + 0.2;
                setRealTimeChart();
            }
            UpdateTextBox("");
            newDataInfor.timeReceived = DateTime.Now.TimeOfDay.ToString().Substring(0, 10);
            lstTemperatureInfo.Add(newDataInfor);
            //sentFireBaseData();
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
            if (checkTorH == 1)
            {
                textBox5.Text = newDataInfor.temperaturePoint.ToString(); ;
            }
            else
            {
                textBox6.Text = newDataInfor.humidityPoint.ToString();
            }          
        }

        private void setChart(ZedGraphControl zedGraphControl, GraphPane graphPane, RollingPointPairList listPoint, double temperature)
        {
            zedGraphControl.GraphPane.CurveList.Clear();
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
            listPoint.Add(timeSeconds, temperature);
            //listPoint = (IPointList)listPoint;
            graphPane.AddCurve("Điểm đo ", listPoint, Color.Red, SymbolType.None);
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
        private void setRealTimeChart()
        {
            if (checkTorH == 1) {
                setChart(zedGraphControl, myPane, listPoint1, newDataInfor.temperaturePoint);
            }else if(checkTorH == 2){
                setChart(zedGraphControl1, myPane1, listPoint2, newDataInfor.humidityPoint);
            }                      
        }     
        /// <summary>
        /// Hàm xử lý giá trị nhận đc từ sensor
        /// </summary>
        /// <param name="checkHorT"></param>
        /// <returns></returns>
        private double getPoint(int checkHorT)
        {
            double value_data = number1 + number2 / 10.0;          
            number1 = 0;
            number2 = 0;
            checkTorH = checkHorT;
            return value_data;
        }
        //Điều khiển máy bơm
        private void button1_Click(object sender, EventArgs e)
        {
            if(dem == 0)
                button1.Text = "ON LED";
            else
            {
                button1.Text = "OFF LED";
            }
            //serialPort.Write(new char[] { '0' }, 0, 1);
            serialPort.Write(new char[] { '0', '1' }, dem, 1);

            dem = (dem + 1) % 2;

        }
        /// <summary>
        /// Hàm xử lý button Ghi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            listView1_SelectedIndexChanged(new object(), new EventArgs());
            this.lstTemperatureInfo = new List<DataInfo>() { };
        }
    
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataInfo data = lstTemperatureInfo.LastOrDefault<DataInfo>();
                addListViewItem(data);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                addListViewItem(cacheDataInfor);
            }
        }

        private void addListViewItem(DataInfo data)
        {
            ListViewItem item1 = new ListViewItem();
            item1.SubItems.Add(data.timeReceived);
            item1.SubItems.Add(textBox5.Text);
            item1.SubItems.Add(textBox6.Text);
            listView1.Items.Add(item1);
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void zedGraphControl_Load(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
    }
    public class DataInfo
    {
        public double temperaturePoint { get; set; }
        public double humidityPoint { get; set; }

        public string timeReceived { get; set; }

        public DataInfo()
        {

        }
    }
}
