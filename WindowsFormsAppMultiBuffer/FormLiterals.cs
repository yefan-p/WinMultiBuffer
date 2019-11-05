using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsAppMultiBuffer
{
    public static class FormLiterals
    {
        /// <summary>
        /// Количество колонок на форме
        /// </summary>
        public static int Column { get; } = 7;
        /// <summary>
        /// Количество строк на форме
        /// </summary>
        public static int Rows { get; } = 3;
        /// <summary>
        /// Размер отступов для элементов формы
        /// </summary>
        public static int Margin { get; } = 20;
        /// <summary>
        /// Коэффициент высоты элементов внутри группы
        /// </summary>
        public static double GroupHeight { get; } = 3.0;
        /// <summary>
        /// Коэффициент ширины элементов внутри группы
        /// </summary>
        public static double GroupWidth { get; } = 1.5;
        /// <summary>
        /// Высота надписей на форме
        /// </summary>
        public static int LabelHeight { get; } = 23;
        /// <summary>
        /// Расположение надписей внутри группы по горизонтальной оси
        /// </summary>
        public static int LabelLocationX { get; } = 6;
        /// <summary>
        /// Расположение надписей внутри группы по вертикальной оси
        /// </summary>
        public static int LabelLocationY { get; } = 10;
        /// <summary>
        /// Расположение текст бокса внутри группы по горизонтальной оси
        /// </summary>
        public static int TextBoxLocationX { get; } = 6;
        /// <summary>
        /// Расположение текст бокса внутри группы по вертикальной оси
        /// </summary>
        public static int TextBoxLocationY { get; } = 33;
        /// <summary>
        /// Коэффициент отступов групп по высоте
        /// </summary>
        public static double GroupHeightMargin { get; } = 0.35;
    }
}
