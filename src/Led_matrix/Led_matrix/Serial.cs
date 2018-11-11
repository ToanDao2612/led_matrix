using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Timers;
using System.Security.Cryptography;

namespace Led_matrix {
    class Serial {

        private SerialPort _serial = null;
        private Timer _timerEvent = null;
        private string[] _serialPort = null;
        public event EventHandler SerialEvent = null;
     
        public Serial() {

            // Init serial port
            _serial = new SerialPort();
            SerialEvent?.Invoke(this, new EventArgs());

            // Init timer to handle serial port change
            _timerEvent = new Timer();
            _serialPort = GetSerialPort();
            _timerEvent.Elapsed += new ElapsedEventHandler(SerialPortPinChanged);
            _timerEvent.Interval = 2000;
            _timerEvent.Start();
        }

        /// <summary>
        /// Get a list of serial port names.
        /// </summary>
        /// <returns>All serial port name</returns>
        public string[] GetSerialPort() {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// Write to the serial port
        /// </summary>
        /// <param name="datas"></param>
        public string WriteDatas(string SerialPort, byte[] datas) {
            try {

                string[] sp = GetSerialPort();
                if (!sp.Contains(SerialPort)) {
                    return "Serial port not available";
                }

                _serial.PortName = "COM11";
                _serial.BaudRate = Convert.ToInt32(9600);
                _serial.Open();
                _serial.Write(datas, 0, datas.Length);
                return "Sent to the device";
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                
            } finally {
                _serial.Close();
            }
            return "Serial port not available";
        }

        /// <summary>
        /// Check is the serial port changed => check every 2 sec
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void SerialPortPinChanged(object source, ElapsedEventArgs e) {
            string[] sp = GetSerialPort();
            if (!sp.SequenceEqual(_serialPort)) {
                _serialPort = sp;
                SerialEvent?.Invoke(this, new EventArgs());
                Console.WriteLine("changed");
            }
        }
    }
}
