using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Input
{

    /// <summary>
    /// Represents the selection of a portion of text
    /// </summary>
    public class TextSelection
        : UIElement
    {

        /// <summary>
        /// Initializes a new <see cref="TextSelection"/> based on the specified <see cref="TextSelectionMode"/>, start index, max left and max right lengths
        /// </summary>
        /// <param name="mode">The selection's <see cref="TextSelectionMode"/></param>
        /// <param name="startIndex">The index of the character from which to start the text selection</param>
        /// <param name="maxLeftLength">An integer representing the maximal length of the selection, on the left handed side</param>
        /// <param name="maxRightLength">An integer representing the maximal length of the selection, on the right handed side</param>
        public TextSelection(TextSelectionMode mode, int startIndex, int maxLeftLength, int maxRightLength)
        {
            this.Mode = mode;
            this.StartIndex = startIndex;
            this.MaxLeftLength = maxLeftLength;
            this.MaxRightLength = maxRightLength;
            this.Length = 1;
        }

        /// <summary>
        /// Gets the selection's <see cref="TextSelectionMode"/>
        /// </summary>
        public TextSelectionMode Mode { get; private set; }

        /// <summary>
        /// Gets the index of the character from which to start the text selection
        /// </summary>
        public int StartIndex { get; private set; }

        /// <summary>
        /// Gets an integer representing the maximal length of the selection, on the left handed side
        /// </summary>
        public int MaxLeftLength { get; private set; }

        /// <summary>
        /// Gets an integer representing the maximal length of the selection, on the right handed side
        /// </summary>
        public int MaxRightLength { get; private set; }

        /// <summary>
        /// Gets an integer representing the current length of the selection
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Gets an integer representing the left handed character index of the text selection
        /// </summary>
        public int LeftIndex
        {
            get
            {
                switch (this.Mode)
                {
                    case TextSelectionMode.LeftHanded:
                        return this.StartIndex - this.Length;
                    case TextSelectionMode.RightHanded:
                        return this.StartIndex;
                    default:
                        return -1;
                }
            }
        }

        /// <summary>
        /// Gets an integer representing the right handed character index of the text selection
        /// </summary>
        public int RightIndex
        {
            get
            {
                switch (this.Mode)
                {
                    case TextSelectionMode.LeftHanded:
                        return this.StartIndex;
                    case TextSelectionMode.RightHanded:
                        return this.StartIndex + this.Length;
                    default:
                        return -1;
                }
            }
        }

        /// <summary>
        /// Gets a string representing the selected text
        /// </summary>
        public string SelectedText
        {
            get
            {
                Controls.ITextPresenter textPresenter;
                if (this.Parent == null)
                {
                    return null;
                }
                textPresenter = (Controls.ITextPresenter)this.Parent;
                return textPresenter.WrappedText.Substring(this.LeftIndex, this.Length);
            }
        }

        /// <summary>
        /// Modifies the text selection according to the specified <see cref="TextSelectionMode"/> and length
        /// </summary>
        /// <param name="mode">The <see cref="TextSelectionMode"/> of the newly selected characters</param>
        /// <param name="length">The amount of newly selected characters</param>
        public void ModifySelection(TextSelectionMode mode, int length)
        {
            switch (mode)
            {
                case TextSelectionMode.LeftHanded:
                    if(this.Mode == TextSelectionMode.LeftHanded)
                    {
                        this.Length += length;
                    }
                    else
                    {
                        this.Length -= length;
                        if(this.Length < 0)
                        {
                            this.Length = -this.Length;
                            if(this.MaxLeftLength > 0)
                            {
                                this.Mode = TextSelectionMode.RightHanded;
                                if (this.Length > this.MaxRightLength)
                                {
                                    this.Length = this.MaxRightLength;
                                }
                            }
                            else
                            {
                                this.Length = 0;
                            }
                        }
                        return;
                    }
                    if (this.Length > this.MaxLeftLength)
                    {
                        this.Length = this.MaxLeftLength;
                    }
                    break;
                case TextSelectionMode.RightHanded:
                    if (this.Mode == TextSelectionMode.RightHanded)
                    {
                        this.Length += length;
                    }
                    else
                    {
                        this.Length -= length;
                        if (this.Length < 0)
                        {
                            this.Length = -this.Length;
                            if(this.MaxRightLength > 0)
                            {
                                this.Mode = TextSelectionMode.LeftHanded;
                                if (this.Length > this.MaxLeftLength)
                                {
                                    this.Length = this.MaxLeftLength;
                                }
                            }
                            else
                            {
                                this.Length = 0;
                            }
                        }
                        return;
                    }
                    if (this.Length > this.MaxRightLength)
                    {
                        this.Length = this.MaxRightLength;
                    }
                    break;
            }
        }

        /// <summary>
        /// Renders the <see cref="TextSelection"/>
        /// </summary>
        /// <param name="drawingContext">The <see cref="DrawingContext"/> in which to render the <see cref="TextSelection"/></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            Controls.ITextPresenter textPresenter;
            Media.Rectangle renderTarget;
            Media.Point textOffset, textPosition;
            int newLineCharIndex, lineIndex, indexInLine, leftIndex;
            double x1, width, y1, height, lineHeight;
            string text, line, tempLine;
            int remains;
            if(this.Parent == null)
            {
                return;
            }
            textPresenter = (Controls.ITextPresenter)this.Parent;
            renderTarget = textPresenter.RenderTarget;
            textOffset = textPresenter.ComputeTextOffset();
            textPosition = renderTarget.Location + textOffset;
            if(this.LeftIndex == 0)
            {
                text = textPresenter.WrappedText.Substring(0, this.RightIndex);
                newLineCharIndex = textPresenter.WrappedText.LastIndexOf(Environment.NewLine, this.LeftIndex);
                leftIndex = 0;
            }
            else
            {
                if(textPresenter.WrappedText[this.LeftIndex] == Environment.NewLine[0]
                    && textPresenter.WrappedText[this.LeftIndex + 1] == Environment.NewLine[1])
                {
                    leftIndex = this.LeftIndex - 3;
                }
                else if(textPresenter.WrappedText[this.LeftIndex] == Environment.NewLine[1]
                    && textPresenter.WrappedText[this.LeftIndex - 1] == Environment.NewLine[0])
                {
                    leftIndex = this.LeftIndex - 2;
                }
                else
                {
                    leftIndex = this.LeftIndex;
                }
            }
            text = textPresenter.WrappedText.Substring(0, leftIndex);
            newLineCharIndex = textPresenter.WrappedText.LastIndexOf(Environment.NewLine, leftIndex);
            line = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Last();
            if (newLineCharIndex == -1)
            {
                newLineCharIndex = 0;
                indexInLine = leftIndex;
            }
            else
            {
                newLineCharIndex += Environment.NewLine.Length;
                indexInLine = leftIndex - newLineCharIndex;
            }
            lineIndex = textPresenter.WrappedText.Substring(0, leftIndex).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Length - 1;
            lineHeight = DrawingContext.MeasureText("A", textPresenter.Font).Height;
            if (indexInLine < 0)
            {
                indexInLine = 0;
            }
            if (this.LeftIndex == 0)
            {
                //There is not first unselected part in the text
                x1 = textPosition.X;
                y1 = textPosition.Y;
            }
            else
            {
                //Draw the first unselected part of the text
                drawingContext.DrawText(textPresenter.WrappedText.Substring(0, leftIndex), textPosition, textPresenter.Font, textPresenter.Foreground);
                tempLine = line.Substring(0, indexInLine);
                x1 = textPosition.X + DrawingContext.MeasureText(tempLine, textPresenter.Font).Width;
                if (this.LeftIndex - newLineCharIndex == 0)
                {
                    y1 = textPosition.Y + (lineHeight * (lineIndex + 1));
                }
                else
                {
                    y1 = textPosition.Y + (lineHeight * lineIndex);
                }
            }
            //Draw the selection box for all the selected lines
            width = 0;
            height = 0;
            remains = 0;
            if (line.Length - indexInLine > this.Length)
            {
                remains = this.Length - (line.Length - indexInLine);
                tempLine = line.Substring(0, indexInLine);
            }
            else
            {
                tempLine = this.SelectedText;
            }
            width = DrawingContext.MeasureText(tempLine, textPresenter.Font).Width;
            height = lineHeight;
            drawingContext.DrawRectangle(new Media.Rectangle(x1, y1, width, height), new Media.Thickness(1), new Media.SolidColorBrush(Color.FromArgb(99, 0, 86, 143)), new Media.SolidColorBrush(Color.FromArgb(255, 0, 86, 143)));
            //Draw the selection box for the remaing selected lines
            while (remains > 0)
            {
                width = 0;
                height = 0;
                newLineCharIndex = newLineCharIndex + line.Length + Environment.NewLine.Length;
                line = textPresenter.WrappedText.Substring(newLineCharIndex).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).First();
                lineIndex++;
                x1 = textPosition.X;
                y1 = textPosition.Y * (lineHeight * lineIndex);
                remains = remains - line.Length;
                if(remains > line.Length)
                {
                    indexInLine = line.Length - 1;
                }
                else
                {
                    indexInLine = remains;
                }
                tempLine = line.Substring(0, indexInLine);
                width = DrawingContext.MeasureText(tempLine, textPresenter.Font).Width;
                height = lineHeight;
                drawingContext.DrawRectangle(new Media.Rectangle(x1, y1, width, height), new Media.Thickness(1), new Media.SolidColorBrush(Color.FromArgb(99, 0, 86, 143)), new Media.SolidColorBrush(Color.FromArgb(255, 0, 86, 143)));
            }
            //Draw the selected text
            drawingContext.DrawText(this.SelectedText, new Media.Point(x1, y1), textPresenter.Font, new Media.SolidColorBrush(Color.White));
            //Draw the last unselected part of the text

            //Draw the last unselected part of the text
            text = textPresenter.WrappedText.Substring(this.RightIndex);
            //drawingContext.DrawText(text, new Media.Point(x2, y2), textPresenter.Font, textPresenter.Foreground);
        }

    }

}
