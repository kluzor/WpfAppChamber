using System;
using System.IO;
using System.Windows;
using System.Data.Odbc;
using EasyModbus;
using System.Data;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace WpfAppChamber
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private double Temperature_Module_APAR;
        private double Humidity_Module_APAR;
        private double Temperature_Act;
        private double Humidity_Act;
        private double Temperature_Set;
        private double Humidity_Set;

        private double _Temperature_Module_APAR;
        private double _Humidity_Module_APAR;
        private double _Temperature_Act;
        private double _Humidity_Act;
        private double _Temperature_Set;
        private double _Humidity_Set;

        private DateTime Date_Time;
        private DateTime _Date_Time;

        private OdbcConnection OdbcConnect;

        private int Temperature_Module_APAR_CH4 = 1;
        private int Humidity_Module_APAR_CH4 = 2;
        private int Temperature_Act_CH4 = 3;
        private int Humidity_Act_CH4 = 4;
        private int Temperature_Set_CH4 = 5;
        private int Humidity_Set_CH4 = 6;

        private int Temperature_Module_APAR_CH5 = 7;
        private int Humidity_Module_APAR_CH5 = 8;
        private int Temperature_Act_CH5 = 9;
        private int Humidity_Act_CH5 = 10;
        private int Temperature_Set_CH5 = 11;
        private int Humidity_Set_CH5 = 12;

        private int Temperature_Module_APAR_CH6 = 13;
        private int Humidity_Module_APAR_CH6 = 14;
        private int Temperature_Act_CH6 = 15;
        private int Humidity_Act_CH6 = 16;
        private int Temperature_Set_CH6 = 17;
        private int Humidity_Set_CH6 = 18;

        private int Temperature_Module_APAR_CH7 = 19;
        private int Humidity_Module_APAR_CH7 = 20;
        private int Temperature_Act_CH7 = 21;
        private int Humidity_Act_CH7 = 22;
        private int Temperature_Set_CH7 = 23;
        private int Humidity_Set_CH7 = 24;

        private int Temperature_Module_APAR_CH15 = 25;
        private int Humidity_Module_APAR_CH15 = 26;
        private int Temperature_Act_CH15 = 27;
        private int Humidity_Act_CH15 = 28;
        private int Temperature_Set_CH15 = 29;
        private int Humidity_Set_CH15 = 30;

        private int Temperature_Module_APAR_CH16 = 31;
        private int Humidity_Module_APAR_CH16 = 32;
        private int Temperature_Act_CH16 = 33;
        private int Humidity_Act_CH16 = 34;
        private int Temperature_Set_CH16 = 35;
        private int Humidity_Set_CH16 = 36;

        private int Temperature_Module_APAR_CH17 = 37;
        private int Humidity_Module_APAR_CH17 = 38;
        private int Temperature_Act_CH17 = 39;
        private int Humidity_Act_CH17 = 40;
        private int Temperature_Set_CH17 = 41;
        private int Humidity_Set_CH17 = 42;

        private int Temperature_Module_APAR_CH18 = 43;
        private int Humidity_Module_APAR_CH18 = 44;
        private int Temperature_Act_CH18 = 45;
        private int Humidity_Act_CH18 = 46;
        private int Temperature_Set_CH18 = 47;
        private int Humidity_Set_CH18 = 48;

        private string Climate_Chamber = "10.1.13.39";
        private int Chamber_Port = 503;

        private bool flagaODBC = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer OdczytZKomoryTimer = new DispatcherTimer();
            OdczytZKomoryTimer.Tick += new EventHandler(OdczytZKomoryTimer_Tick);
            OdczytZKomoryTimer.Interval = new TimeSpan(0, 1, 0);
            OdczytZKomoryTimer.Start();

            DispatcherTimer InsertRowTimer = new DispatcherTimer();
            InsertRowTimer.Tick += new EventHandler(InsertRowTimer_Tick);
            InsertRowTimer.Interval = new TimeSpan(0, 5, 3);
            InsertRowTimer.Start();

            DataBaseConnect();
        }

        private void OdczytZKomoryTimer_Tick(object sender, EventArgs e)
        {
            OdczytZKomory("CHAMBER4", Climate_Chamber, Chamber_Port, Temperature_Module_APAR_CH4, Humidity_Module_APAR_CH4, Temperature_Act_CH4, Humidity_Act_CH4, Temperature_Set_CH4, Humidity_Set_CH4);
            OdczytZKomory("CHAMBER5", Climate_Chamber, Chamber_Port, Temperature_Module_APAR_CH5, Humidity_Module_APAR_CH5, Temperature_Act_CH5, Humidity_Act_CH5, Temperature_Set_CH5, Humidity_Set_CH5);;
            OdczytZKomory("CHAMBER6", Climate_Chamber, Chamber_Port, Temperature_Module_APAR_CH6, Humidity_Module_APAR_CH6, Temperature_Act_CH6, Humidity_Act_CH6, Temperature_Set_CH6, Humidity_Set_CH6);
            OdczytZKomory("CHAMBER7", Climate_Chamber, Chamber_Port, Temperature_Module_APAR_CH7, Humidity_Module_APAR_CH7, Temperature_Act_CH7, Humidity_Act_CH7, Temperature_Set_CH7, Humidity_Set_CH7);
            OdczytZKomory("CHAMBER15", Climate_Chamber, Chamber_Port, Temperature_Module_APAR_CH15, Humidity_Module_APAR_CH15, Temperature_Act_CH15, Humidity_Act_CH15, Temperature_Set_CH15, Humidity_Set_CH15);
            OdczytZKomory("CHAMBER16", Climate_Chamber, Chamber_Port, Temperature_Module_APAR_CH16, Humidity_Module_APAR_CH16, Temperature_Act_CH16, Humidity_Act_CH16, Temperature_Set_CH16, Humidity_Set_CH16);
            OdczytZKomory("CHAMBER17", Climate_Chamber, Chamber_Port, Temperature_Module_APAR_CH17, Humidity_Module_APAR_CH17, Temperature_Act_CH17, Humidity_Act_CH17, Temperature_Set_CH17, Humidity_Set_CH17);
            OdczytZKomory("CHAMBER18", Climate_Chamber, Chamber_Port, Temperature_Module_APAR_CH18, Humidity_Module_APAR_CH18, Temperature_Act_CH18, Humidity_Act_CH18, Temperature_Set_CH18, Humidity_Set_CH18);
        }

        private void InsertRowTimer_Tick(object sender, EventArgs e)
        {
            if (flagaODBC == false)
            {
                DataBaseConnect();
            }
            else
            {
                odczytZPliku("CHAMBER4");
                odczytZPliku("CHAMBER5");
                odczytZPliku("CHAMBER6");
                odczytZPliku("CHAMBER7");
                odczytZPliku("CHAMBER15");
                odczytZPliku("CHAMBER16");
                odczytZPliku("CHAMBER17");
                odczytZPliku("CHAMBER18");
            }
        }

        private async void DataBaseConnect()
        {
            await Task.Run(() =>
            {
                try
                {
                    var connectionString = @"DSN=PLC_SQL;DatabaseName=PLC_DB;UID=PLC;PWD=plc_pwd;";
                    OdbcConnect = new OdbcConnection(connectionString);
                    OdbcConnect.ConnectionTimeout = 5;
                    OdbcConnect.Open();
                    flagaODBC = true;
                }
                catch (Exception ex)
                {
                   flagaODBC = false;
                   zapiszLog("Błąd podczas DataBaseConnect: " + ex.Message);
                }
            });
        }

        private void InsertRow(string CHAMBER_NO)
        {
            int rowsAffected = 0;

            if ((Temperature_Module_APAR == 0) && (Humidity_Module_APAR == 0) && (Temperature_Act == 0) && (Humidity_Act == 0) && (Temperature_Set == 0) && (Humidity_Set == 0))
            {
                zapiszLog("Zera podczas wykonywania INSERTA");
            }
            else
            {
                if (flagaODBC == true)
                {
                    try
                    {
                        OdbcCommand insert = new OdbcCommand("INSERT INTO " + CHAMBER_NO + "(Temperature_Module_APAR, Humidity_Module_APAR," +
                            " Temperature_Act, Humidity_Act, Temperature_Set, Humidity_Set, Date_Time) VALUES(?,?,?,?,?,?,?)", OdbcConnect);

                        insert.Parameters.Add("@field1", OdbcType.Double);
                        insert.Parameters.Add("@field2", OdbcType.Double);
                        insert.Parameters.Add("@field3", OdbcType.Double);
                        insert.Parameters.Add("@field4", OdbcType.Double);
                        insert.Parameters.Add("@field5", OdbcType.Double);
                        insert.Parameters.Add("@field6", OdbcType.Double);
                        insert.Parameters.Add("@field7", OdbcType.DateTime);

                        insert.Parameters["@field1"].Value = Temperature_Module_APAR;
                        insert.Parameters["@field2"].Value = Humidity_Module_APAR;
                        insert.Parameters["@field3"].Value = Temperature_Act;
                        insert.Parameters["@field4"].Value = Humidity_Act;
                        insert.Parameters["@field5"].Value = Temperature_Set;
                        insert.Parameters["@field6"].Value = Humidity_Set;
                        insert.Parameters["@field7"].Value = Date_Time;

                        rowsAffected = insert.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            if (File.Exists(CHAMBER_NO + ".txt"))
                            {
                                File.Delete(CHAMBER_NO + ".txt");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        zapiszLog("Błąd podczas InsertRow: " + ex.Message);
                        DataBaseConnect();
                    }
                }
                else
                {
                    zapiszDoPliku(CHAMBER_NO);
                }
            }
        }

        private async void odczytZPliku(string CHAMBER_NO)
        {
            if (File.Exists(CHAMBER_NO + ".txt"))
            {
                List<Dane> dane = new List<Dane>();
                string line;

                try
                {
                    UnicodeEncoding uniencoding = new UnicodeEncoding();
                    FileStream SourceStream = File.Open((CHAMBER_NO + ".txt"), FileMode.Open, FileAccess.Read, FileShare.Write);
                    StreamReader sr = new StreamReader(SourceStream, uniencoding);

                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        string[] s = line.Split('^');
                        var _dane = new Dane
                        {
                            _Temperature_Module_APAR = double.Parse(s[0]),
                            _Humidity_Module_APAR = double.Parse(s[1]),
                            _Temperature_Act = double.Parse(s[2]),
                            _Humidity_Act = double.Parse(s[3]),
                            _Temperature_Set = double.Parse(s[4]),
                            _Humidity_Set = double.Parse(s[5]),
                            _Date_Time = DateTime.Parse(s[6])
                        };

                        dane.Add(_dane);
                        _Temperature_Module_APAR = _dane._Temperature_Module_APAR;
                        _Humidity_Module_APAR = _dane._Humidity_Module_APAR;
                        _Temperature_Act = _dane._Temperature_Act;
                        _Humidity_Act = _dane._Humidity_Act;
                        _Temperature_Set = _dane._Temperature_Set;
                        _Humidity_Set = _dane._Humidity_Set;
                        _Date_Time = _dane._Date_Time;

                        Temperature_Module_APAR = _Temperature_Module_APAR;
                        Humidity_Module_APAR = _Humidity_Module_APAR;
                        Temperature_Act = _Temperature_Act;
                        Humidity_Act = _Humidity_Act;
                        Temperature_Set = _Temperature_Set;
                        Humidity_Set = _Humidity_Set;
                        Date_Time = _Date_Time;

                        InsertRow(CHAMBER_NO);
                    }
                    sr.Dispose();
                    sr.Close();

                    SourceStream.Dispose();
                    SourceStream.Close();
                }
                catch (Exception ex)
                {
                    zapiszLog("Błąd podczas odczytZPliku: " + ex.Message);
                }
            }
        }

        private async void zapiszDoPliku(string nazwaPliku)
        {
            try
            {
                UnicodeEncoding uniencoding = new UnicodeEncoding();
                byte[] result = uniencoding.GetBytes(Temperature_Module_APAR + "^" + Humidity_Module_APAR + "^"
                                + Temperature_Act + "^" + Humidity_Act + "^" + Temperature_Set + "^" + Humidity_Set + "^" + Date_Time + "\r\n");
                FileStream writer = File.Open(nazwaPliku + ".txt", FileMode.Append, FileAccess.Write, FileShare.Read);
                await writer.WriteAsync(result, 0, result.Length);
                writer.Dispose();
                writer.Close();
            }
            catch (Exception ex)
            {
                zapiszLog("Błąd podczas zapiszDoPliku: " + ex.Message);
            }
        }

        private async void zapiszLog(string logResult)
        {
            UnicodeEncoding uniencoding = new UnicodeEncoding();
            byte[] result = uniencoding.GetBytes(DateTime.Now + " " + logResult + "\r\n");
            FileStream writer = File.Open("log.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            await writer.WriteAsync(result, 0, result.Length); ;
            writer.Dispose();
            writer.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataBaseConnect();
        }

        private void OdczytZKomory(string CHAMBER_NO, string IP, int Port, int _Temperature_Module_APAR, int _Humidity_Module_APAR, int _Temperature_Act, int _Humidity_Act, int _Temperature_Set, int _Humidity_Set)
        {
            try
            {
                double nTemperature_Module_APAR = OdczytajDanaRejestru(IP, Port, _Temperature_Module_APAR, 1);
                double nHumidity_Module_APAR = OdczytajDanaRejestru(IP, Port, _Humidity_Module_APAR, 1);
                double nTemperature_Act = OdczytajDanaRejestru(IP, Port, _Temperature_Act, 1);
                double nHumidity_Act = OdczytajDanaRejestru(IP, Port, _Humidity_Act, 1);
                double nTemperature_Set = OdczytajDanaRejestru(IP, Port, _Temperature_Set, 1);
                double nHumidity_Set = OdczytajDanaRejestru(IP, Port, _Humidity_Set, 1);

                Temperature_Module_APAR = nTemperature_Module_APAR / 10;
                Humidity_Module_APAR = nHumidity_Module_APAR / 10;
                Temperature_Act = nTemperature_Act / 10;
                Humidity_Act = nHumidity_Act / 10;
                Temperature_Set = nTemperature_Set / 10;
                Humidity_Set = nHumidity_Set / 10;
                Date_Time = DateTime.Now;

                InsertRow(CHAMBER_NO);
            }
            catch (Exception ex)
            {
                zapiszLog("Błąd podczas OdczytZKomory " + ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private int OdczytajDanaRejestru(string plcIP, int port, int adresRejestru, int iloscRejestrow)
        {
            int[] wynik = new int[iloscRejestrow];

            try
            {
                ModbusClient modbusClient = new ModbusClient(plcIP, port); //Ip-Address and Port of Modbus-TCP-Server
                modbusClient.ConnectionTimeout = 5000; //Increase the Connection Timeout to 5 seconds
                modbusClient.Connect(); //Connect to Server
                int[] readHoldingRegisters = modbusClient.ReadHoldingRegisters((adresRejestru - 1), iloscRejestrow); //Read one or more Holding Registers from Server, starting with Address

                for (int i = 0; i < readHoldingRegisters.Length; i++)
                {
                    wynik[i] = readHoldingRegisters[i];
                }
                modbusClient.Disconnect(); //Disconnect from Server
            }
            catch(Exception)
            {
                zapiszLog("Błąd podczas OdczytajDanaRejestru");
            }
            return wynik[0];
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //użyć ExecuteReaderAsync
            //przebudować generowanie raportu
            try
            {
                OdbcCommand select = new OdbcCommand("SELECT * FROM " + ChamberTextBlock.Text + " ORDER BY DATE_TIME DESC ", OdbcConnect);
                var result = await select.ExecuteNonQueryAsync();

                OdbcDataReader odbcDataReader = select.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(odbcDataReader);
                DataView.ItemsSource = dt.DefaultView;
                odbcDataReader.Dispose();
                odbcDataReader.Close();
            }
            catch (OdbcException ex)
            {
                zapiszLog("Błąd wykonania polecenia SELECT: " + ex.Message);
                MessageBox.Show("Brak połączenia z bazą danych");
            }
        }
    }
}