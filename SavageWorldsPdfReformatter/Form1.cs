using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SavageWorldsPdfReformatter
{
    public partial class Form1 : Form
    {
        const char Apostrophe = '’';
        const char ApostropheReplacement = '\'';
        const string Hyphen = "—";
        const string HyphenReplacement = " - ";
        const char Dash = '-';
        const char DashReplacement = '-';
        const string Bullet = "• ";
        const string BulletReplacement = "\r•\t";
        const string DoubleLineBreak = "\n\n";
        const string DoubleLineBreakPlaceholder = "_DOUBLE_LINEBREAK_PLACEHOLDER_";
        const string LineBreak = "\n";
        const string LineBreakReplacement = " ";
        const string SpaceAtEndOfLine = " \r";
        const string SpaceAtStartOfLine = "\r ";
        const string NewLineBreak = "\r";

        const int WidthOffset = 49;

        public Form1()
        {
            InitializeComponent();
            ResizeTextBoxes();
        }

        private void richTextBoxInput_TextChanged(object sender, EventArgs e)
        {
            ReformatText();
            CopyOutputToClipboard();
            richTextBoxInput.SelectAll();
        }

        private void CopyOutputToClipboard()
        {
            if (richTextBoxOutput.Text != string.Empty)
            {
                Clipboard.SetText(richTextBoxOutput.Text);
            }
        }

        private void ReformatText()
        {
            string text = richTextBoxInput.Text;
            string reformattedText = text
                .Replace(Apostrophe, ApostropheReplacement)
                .Replace(Hyphen, HyphenReplacement)
                .Replace(Dash, DashReplacement)
                .Replace(Bullet, BulletReplacement)
                .Replace(DoubleLineBreak, DoubleLineBreakPlaceholder)
                .Replace(LineBreak, LineBreakReplacement)
                .Replace(DoubleLineBreakPlaceholder, NewLineBreak)
                .Replace(SpaceAtEndOfLine, NewLineBreak)
                .Replace(SpaceAtStartOfLine, NewLineBreak)
                .Replace("armor", "armour")
                .Replace("Armor", "Armour")
                .Replace("color", "colour")
                .Replace("Color", "Colour")
                .Replace("odor", "odour")
                .Replace("Odor", "Odour")
                .Replace("center", "centre")
                .Replace("Center", "Centre")
                .Replace("favorite", "favourite")
                .Replace("Favorite", "Favourite")
                .Trim();
            richTextBoxOutput.Text = Regex.Replace(reformattedText, @"[ ]{2,}", " ");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeTextBoxes();
        }

        private void ResizeTextBoxes()
        {
            int formWidth = this.Width;
            int textBoxWidth = (formWidth - WidthOffset) / 2;
            int outbutBoxOriginalWidth = richTextBoxOutput.Width;
            richTextBoxInput.Width = textBoxWidth;
            richTextBoxOutput.Width = textBoxWidth;
            richTextBoxOutput.Location = new Point(richTextBoxOutput.Location.X - textBoxWidth + outbutBoxOriginalWidth, richTextBoxOutput.Location.Y);
        }
    }
}
