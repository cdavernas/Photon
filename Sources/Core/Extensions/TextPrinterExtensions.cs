using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// This static class defines extensions methods for the <see cref="TextPrinter"/> type
    /// </summary>
    public static class TextPrinterExtensions
    {

        /// <summary>
        /// Wraps the text according to the specified width and height
        /// </summary>
        /// <param name="extended">The extended <see cref="TextPrinter"/></param>
        /// <param name="textToWrap">A string representing the text to wrap</param>
        /// <param name="font">The <see cref="Font"/> thanks to which the text is rendered</param>
        /// <param name="width">A double representing the maximal width of the wrapped text</param>
        /// <param name="height">A double representing the maximal height of the wrapped text</param>
        /// <returns>The wrapped text</returns>
        #pragma warning disable 612, 618
        public static string WrapText(this TextPrinter extended, string textToWrap, Font font, double? width, double? height)
        #pragma warning restore 612, 618
        {
            StringBuilder writer;
            int position, next, endOfLine, length, lineCount;
            string currentText;
            double currentWidth;
            if (!width.HasValue
                || string.IsNullOrEmpty(textToWrap))
            {
                return textToWrap;
            }
            writer = new StringBuilder();
            lineCount = 0;
            for (position = 0; position < textToWrap.Length; position = next)
            {
                endOfLine = textToWrap.IndexOf(Environment.NewLine, position);
                if (endOfLine == -1)
                {
                    next = endOfLine = textToWrap.Length;
                }
                else
                {
                    next = endOfLine + Environment.NewLine.Length;
                }
                if (position < endOfLine)
                {
                    while(position < endOfLine)
                    {
                        length = endOfLine - position;
                        currentText = textToWrap.Substring(position, length);
                        currentWidth = DrawingContext.MeasureText(currentText, font).Width;
                        if(currentWidth > width.Value)
                        {
                            length = TextPrinterExtensions.BreakLine(textToWrap, font, position, width.Value);
                        }
                        writer.Append(textToWrap, position, length);
                        writer.Append(Environment.NewLine);
                        position += length;
                        lineCount++;
                        while (position < endOfLine 
                            && Char.IsWhiteSpace(textToWrap[position]))
                        {
                            position++;
                        }
                    }
                }
                else
                {
                    writer.Append(Environment.NewLine);
                }
            }
            return writer.ToString();
        }

        /// <summary>
        /// Breaks the specified line into a line of a specified maximal width, starting from the specified position
        /// </summary>
        /// <param name="line">A string representing the line to break</param>
        /// <param name="font">The <see cref="Font"/> thanks to which the text is rendered</param>
        /// <param name="position">The position from which to start breaking the line</param>
        /// <param name="maxWidth">The resulting line's max width</param>
        /// <returns>The broken line</returns>
        private static int BreakLine(string line, Font font, int position, double maxWidth)
        {
            int length;
            string currentText;
            double currentWidth;
            length = 0;
            currentText = null;
            currentWidth = 0;
            while(currentWidth < maxWidth)
            {
                currentText += line[position + length];
                currentWidth = DrawingContext.MeasureText(currentText, font).Width;
                length++;
            }
            return length;
        }

    }

}
