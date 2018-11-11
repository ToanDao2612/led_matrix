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

namespace Led_matrix {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        // Led value
        private Color ON = Color.Red;
        private Color OFF = Color.White;

        // Serial to send on COM port
        Serial serial = null;

        /// <summary>
        /// Load serial and get all serial ports
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e) {
            serial = new Serial();
            serial.SerialEvent += new EventHandler(SerialEvent);
            CB_serial_port.Invoke(new Action(() => InvokeSerialPort()));
        }

        /// <summary>
        /// EventHandler for all leds buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Led_Click(object sender, EventArgs e) {

            // Change button color
            if ((sender as Button).BackColor == ON) {
                (sender as Button).BackColor = OFF;
            } else {
                (sender as Button).BackColor = ON;
            }
            GenLedByte();
        }

        /// <summary>
        /// Generate string of byte array (leds position)
        /// </summary>
        private void GenLedByte() {
            List<string> leds_values = new List<string>();
            int leds_value = 0;
            for (int line = 0; line < 8; line++) {
                for (int col = 0; col < 8; col++) {

                    // Find button
                    Button button = this.Controls.Find($"led_{line}{col}", true).FirstOrDefault() as Button;

                    // Check if exist
                    if (button != null) {

                        // Check if selected (color red)
                        if (button.BackColor == ON) {
                            leds_value += (int)Math.Pow(2, col % 8);
                        } else {
                            leds_value += 0;
                        }
                    }
                }
                leds_values.Add($"0x{leds_value.ToString("X2")}");
                leds_value = 0;
            }
            TB_leds_byte.Text =  $"{string.Join(",", leds_values.ToArray())}";
        }

        /// <summary>
        /// Clear or fill all leds
        /// </summary>
        /// <param name="on">true: fill all leds; false: clear all leds</param>
        private void FillLed(bool on) {
            for (int line = 0; line < 8; line++) {
                for (int col = 0; col < 8; col++) {

                    // Find and check if exist
                    Button button = this.Controls.Find($"led_{line}{col}", true).FirstOrDefault() as Button;
                    if (button != null) {   
                        if (on) {
                            button.BackColor = ON;
                        } else {
                            button.BackColor = OFF;
                        }
                    }
                }
            }
            GenLedByte();
        }

        /// <summary>
        /// Copy leds string of byte array (leds position) to clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_copy_Click(object sender, EventArgs e) {
            Clipboard.SetText(TB_leds_byte.Text);
            TSSL_message.Text = DateTime.Now.ToString("hh:mm:ss") + $" Copied to clipboard";
        }

        /// <summary>
        /// Fill all leds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_select_all_Click(object sender, EventArgs e) {
            FillLed(true);
        }

        /// <summary>
        /// Clear all leds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_clear_Click(object sender, EventArgs e) {
            FillLed(false);
        }

        /// <summary>
        /// Send to the serial port the leds matrix datas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_send_Click(object sender, EventArgs e) {
            List<byte> leds = new List<byte>();
            foreach (string str in TB_leds_byte.Text.Split(',')) {
                leds.Add(byte.Parse(str.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber));
            }
            TSSL_message.Text = DateTime.Now.ToString("hh:mm:ss") + $" {serial.WriteDatas(CB_serial_port.Text, leds.ToArray())}";
        }

        /// <summary>
        /// Event when the serial port change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialEvent(object sender, EventArgs e) {
            CB_serial_port.Invoke(new Action(() => InvokeSerialPort()));
        }

        /// <summary>
        /// Modify the combobox with the new serial port values
        /// </summary>
        private void InvokeSerialPort() {
            string[] sp = serial.GetSerialPort();
            string def = "Select serial port";
            if (CB_serial_port.Text != def) {

                // If user select serial port and existe
                if (!sp.Contains(CB_serial_port.Text)) {
                    CB_serial_port.Items.Clear();
                    CB_serial_port.Text = def;
                    CB_serial_port.Items.AddRange(sp);
                } else {
                    CB_serial_port.Items.Clear();
                    CB_serial_port.Items.AddRange(sp);
                }         
            } else {
                CB_serial_port.Items.Clear();
                CB_serial_port.Text = def;
                CB_serial_port.Items.AddRange(sp);
            }
        }
    }
}
