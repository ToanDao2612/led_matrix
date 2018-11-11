using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Led_matrix {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LedClick(object sender, EventArgs e) {

            // Change button color
            if ((sender as Button).BackColor == Color.Red) {
                (sender as Button).BackColor = Color.White;
            } else {
                (sender as Button).BackColor = Color.Red;
            }

            // Genrate new led byte array
            Gen_led_byte();
        }


        /// <summary>
        /// 
        /// </summary>
        private void Gen_led_byte() {

            List<string> leds_values = new List<string>();
            int leds_value = 0;


            for (int line = 0; line < 8; line++) {
                for (int col = 0; col < 8; col++) {

                    // Find button
                    Button button = this.Controls.Find($"led_{line}{col}", true).FirstOrDefault() as Button;

                    // Check if exist
                    if (button != null) {

                        // Check if selected (color red)
                        if (button.BackColor == Color.Red) {
                            leds_value += (int)Math.Pow(2, col % 8);
                        } else {
                            leds_value += (int)0;
                        }
                    }
                }
                leds_values.Add($"0x{leds_value.ToString("X2")}");
                leds_value = 0;
            }
            TB_leds_byte.Text =  $"{string.Join(",", leds_values.ToArray())}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="on"></param>
        private void Fill_led(bool on) {
            for (int line = 0; line < 8; line++) {
                for (int col = 0; col < 8; col++) {

                    // Find button
                    Button button = this.Controls.Find($"led_{line}{col}", true).FirstOrDefault() as Button;

                    // Check if exist
                    if (button != null) {


                        if (on) {
                            button.BackColor = Color.Red;
                        } else {
                            button.BackColor = Color.White;
                        }
                    }
                }
            }
            Gen_led_byte();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_copy_Click(object sender, EventArgs e) {
            Clipboard.SetText(TB_leds_byte.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_select_all_Click(object sender, EventArgs e) {
            Fill_led(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_clear_Click(object sender, EventArgs e) {
            Fill_led(false);
        }
    }
}
