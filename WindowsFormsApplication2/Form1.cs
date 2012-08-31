using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using System.IO;
using System.Media;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private string[] sound = new string[26];
        private SoundPlayer player;
        private bool toggleSound = true;
        private bool findMode = false;
        private bool wordMode = false;
        private bool found = false;
        private SpeechSynthesizer speak = new SpeechSynthesizer();
        private char letter;
        private int wordLenght = 5;

        public Form1()
        {
            InitializeComponent();

            int index = 97;
            for(int i = 0; i < 26; i++)
            {
                sound[i] = "Music\\" + (char)(i+index) + ".wav";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            string textToSpeak;
            int last = 0;
            textToSpeak = textBox1.Text.ToLower();
            last = textToSpeak.Length - 1;

            if (last >= 0)
            {
                var location = textToSpeak[last];

                if (location >= 97 && location <= 122)
                {
                    player = new SoundPlayer();

                    player.SoundLocation = sound[location - 97].ToString();

                    if (toggleSound)
                    {
                        player.Play();
                    }
                }
            }

            if (findMode)
            {
                var answer = textBox1.Text;
                if (answer == letter.ToString())
                {
                    found = true;
                }

                if (found)
                {
                    speak.Speak("good job you found it");
                    find();
                }
                else if(!found && textBox1.Text != "")
                {
                    speak.Speak("That is not correct");
                }

                //textBox1.Text = null;
            }

            if (wordMode)
            {
                if (wordLenght == textBox1.Text.Length)
                {
                    player.Stop();
                    speak.Speak(textBox1.Text);
                    textBox1.Text = null;
                    textBox1.Focus();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Sound On")
            {
                button1.Text = "Sound Off";
                toggleSound = false;
            }
            else
            {
                button1.Text = "Sound On";
                toggleSound = true;
            }
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.Focus();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                textBox1.ForeColor = colorDialog1.Color;
            }
            textBox1.Focus();
        }

        //play text
        private void button4_Click(object sender, EventArgs e)
        {
            speak.Volume = 100;
            speak.Rate = -3;
            if (toggleSound)
            {
                if (textBox1.Text.Length < 32)
                {
                    speak.Speak(textBox1.Text);
                }
                else
                {
                    speak.Speak("good job typing");
                }
                textBox1.Text = null;
                textBox1.Focus();
            }
        }

        //find the letter
        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "Find Mode On")
            {
                button5.Text = "Find Mode Off";
                findMode = false;
            }
            else
            {
                textBox1.Text = null;
                button5.Text = "Find Mode On";
                findMode = true;
            }

            find();
        }

        void find()
        {
            textBox1.Text = null;
            found = false;

            Random rand = new Random((int)DateTime.Now.Ticks);
            var randLetter = rand.Next(97, 122);
            letter = (char)randLetter;

            string question = "Find the letter ";

            label1.Text = question;
            label2.Text = letter.ToString();

            speak.Speak(label1.Text);
            player = new SoundPlayer();
            player.SoundLocation = sound[randLetter - 97].ToString();
            player.Play();

            textBox1.Focus();
        }

        //word mode
        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "Word Mode On")
            {
                button6.Text = "Word Mode Off";
                wordMode = false;
            }
            else
            {
                textBox1.Text = null;
                button6.Text = "Word Mode On";
                wordMode = true;
            }
            textBox1.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            wordLenght = Convert.ToInt32(comboBox1.Text);
        }

    }
}
