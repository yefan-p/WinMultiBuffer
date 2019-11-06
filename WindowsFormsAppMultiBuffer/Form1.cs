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
            CreateView(_buffer.Storage);
            Buffer_BufferUpdate(this, new MultiBufferEventArgs(_buffer.Storage));

            _buffer.BufferUpdate += Buffer_BufferUpdate;
            Resize += (sender, arg) => Buffer_BufferUpdate(sender, new MultiBufferEventArgs(_buffer.Storage));
        }

        private void Buffer_BufferUpdate(object sender, MultiBufferEventArgs e)
        {
            int currentCol = 0;
            int currentRow = 0;

            int groupBoxWidth = (Width / FormLiterals.Column) - FormLiterals.Margin;
            int groupBoxHeight = (Height / FormLiterals.Rows) - FormLiterals.Margin;
            int labelWidth = (int)Math.Ceiling((Width / FormLiterals.Column) - (FormLiterals.Margin * FormLiterals.GroupWidth));
            int textBoxWidth = (int)Math.Ceiling((Width / FormLiterals.Column) - (FormLiterals.Margin * FormLiterals.GroupWidth));
            int textBoxHeight = (int)Math.Ceiling((Height / FormLiterals.Rows) - (FormLiterals.Margin * FormLiterals.GroupHeight));

            foreach (TwiceKeyDictionaryItem<Keys, string> item in e.Storage)
            {
                GroupBox groupBox = Controls.OfType<GroupBox>()
                                            .Where(el =>
                                            {
                                                TwiceKeyDictionaryItem<Keys, string> itemTag = (TwiceKeyDictionaryItem<Keys, string>)el.Tag;
                                                if (itemTag.FirtsKey == item.FirtsKey
                                                        && itemTag.SecondKey == item.SecondKey
                                                        && itemTag.Value == item.Value)
                                                    return true;
                                                else
                                                    return false;
                                            }).First();

                int groupBoxLocationX = (groupBoxWidth + FormLiterals.Margin) * currentCol;
                int groupBoxLocationY = (int)Math.Ceiling((groupBoxHeight + FormLiterals.Margin * FormLiterals.GroupHeightMargin) * currentRow);
                groupBox.Location = new Point(groupBoxLocationX, groupBoxLocationY);
                groupBox.Size = new Size(groupBoxWidth, groupBoxHeight);
                currentCol++;
                if (currentCol == FormLiterals.Column)
                {
                    currentCol = 0;
                    currentRow++;
                }

                Label label = groupBox.Controls.OfType<Label>().First();
                label.Size = new Size(labelWidth, FormLiterals.LabelHeight);
                label.Location = new Point(FormLiterals.LabelLocationX, FormLiterals.LabelLocationY);
                label.Text = $"{item.FirtsKey} / {item.SecondKey}";

                TextBox text = groupBox.Controls.OfType<TextBox>().First();
                text.Size = new Size(textBoxWidth, textBoxHeight);
                text.Multiline = true;
                text.Location = new Point(FormLiterals.TextBoxLocationX, FormLiterals.TextBoxLocationY);
                text.ReadOnly = true;
                text.Text = item.Value;

                groupBox.Controls.Add(label);
                groupBox.Controls.Add(text);
                Controls.Add(groupBox);
            }
        }

        private void CreateView(TwiceKeyDictionary<Keys, string> buffer)
        {
            foreach (TwiceKeyDictionaryItem<Keys, string> item in buffer)
            {
                GroupBox groupBox = new GroupBox();
                groupBox.Tag = item;

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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _buffer.Dispose();
        }
    }
}

