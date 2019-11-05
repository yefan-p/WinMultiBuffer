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
        MultiBuffer _buffer;

        public Form1()
        {
            InitializeComponent();

            _buffer = new MultiBuffer();
            _buffer.BufferUpdate += Buffer_BufferUpdate;
            UpdateView(_buffer.Storage);
        }

        private void Buffer_BufferUpdate(object sender, MultiBufferEventArgs e)
        {
            UpdateView(e.Storage);
        }

        private void CreateView(TwiceKeyDictionary<Keys, string> buffer)
        {
            foreach (TwiceKeyDictionaryItem<Keys, string> item in buffer)
            {
                GroupBox groupBox = new GroupBox();

                Label label = new Label();
                label.Text = $"{item.FirtsKey} / {item.SecondKey}";
                
                TextBox text = new TextBox
                {
                    Multiline = true,
                    ReadOnly = true,
                    Text = item.Value,
                };

                groupBox.Controls.Add(label);
                groupBox.Controls.Add(text);
                Controls.Add(groupBox);
            }
        }

        private void UpdateView(TwiceKeyDictionary<Keys, string> buffer)
        {
            int currentCol = 0;
            int currentRow = 0;

            foreach (TwiceKeyDictionaryItem<Keys, string> item in buffer)
            {
                GroupBox groupBox = new GroupBox();
                int groupBoxWidth = (Width / FormLiterals.Column) - FormLiterals.Margin;
                int groupBoxHeight = (Height / FormLiterals.Rows) - FormLiterals.Margin;
                groupBox.Size = new Size(groupBoxWidth, groupBoxHeight);
                int groupBoxLocationX = (groupBoxWidth + FormLiterals.Margin) * currentCol;
                int groupBoxLocationY = (int)Math.Ceiling((groupBoxHeight + FormLiterals.Margin * FormLiterals.GroupHeightMargin) * currentRow);
                groupBox.Location = new Point(groupBoxLocationX, groupBoxLocationY);
                currentCol++;
                if (currentCol == FormLiterals.Column)
                {
                    currentCol = 0;
                    currentRow++;
                }

                int labelWidth = (int)Math.Ceiling((Width / FormLiterals.Column) - (FormLiterals.Margin * FormLiterals.GroupWidth));
                Label label = new Label
                {
                    Size = new Size(labelWidth, FormLiterals.LabelHeight),
                    Location = new Point(FormLiterals.LabelLocationX, FormLiterals.LabelLocationY),
                    Text = $"{item.FirtsKey} / {item.SecondKey}",
                };

                int textBoxWidth = (int)Math.Ceiling((Width / FormLiterals.Column) - (FormLiterals.Margin * FormLiterals.GroupWidth));
                int textBoxHeight = (int)Math.Ceiling((Height / FormLiterals.Rows) - (FormLiterals.Margin * FormLiterals.GroupHeight));
                TextBox text = new TextBox
                {
                    Size = new Size(textBoxWidth, textBoxHeight),
                    Multiline = true,
                    Location = new Point(FormLiterals.TextBoxLocationX, FormLiterals.TextBoxLocationY),
                    ReadOnly = true,
                    Text = item.Value,
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
                Label label = item.Controls.OfType<Label>().First();
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
            _buffer.Dispose();
        }
    }
}

