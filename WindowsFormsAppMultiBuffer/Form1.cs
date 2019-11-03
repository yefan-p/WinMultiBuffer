using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppMultiBuffer
{

    public partial class Form1 : Form
    {
        MultiBuffer buffer;

        public Form1()
        {
            InitializeComponent();

            buffer = new MultiBuffer();
            buffer.BufferUpdate += Buffer_BufferUpdate;
            UpdateView(buffer.Storage);
        }

        private void Buffer_BufferUpdate(object sender, MultiBufferEventArgs e)
        {
            UpdateView(e.Storage);
        }

        private void UpdateView(TwiceKeyDictionary<Keys, string> buffer)
        {
            Controls.Clear();

            foreach (var item in buffer)
            {
                GroupBox groupBox = new GroupBox();
                groupBox.Size = new Size(200, 281);

                Label label = new Label
                {
                    Size = new Size(188, 23),
                    Location = new Point(6, 19),
                    Text = $"{item.Key} / {item.Value}",
                };

                TextBox text = new TextBox
                {
                    Size = new Size(188, 219),
                    Multiline = true,
                    Location = new Point(6, 50),
                    ReadOnly = true,
                    Text = buffer.Values[item.Value],
                };

                groupBox.Controls.Add(label);
                groupBox.Controls.Add(text);

                Controls.Add(groupBox);
            }

                Resize += Form1_Resize;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int WidthPadding = 212; // ширина группы с отсутом
            int HeightPadding = 293; // высота группы с отсутом

            int MaxCountCol = Width / WidthPadding;
            int CurrentCol = 0;
            int CurrentRow = 0;

            foreach (GroupBox item in Controls.OfType<GroupBox>())
            {
                item.Location = new Point(CurrentCol * WidthPadding, CurrentRow * HeightPadding);
                CurrentCol++;
                if (CurrentCol == MaxCountCol)
                {
                    CurrentCol = 0;
                    CurrentRow++;
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            buffer.Dispose();
        }
    }
}

