﻿using System.Windows.Input;

namespace WpfAppMultiBuffer
{
    public static class ParseChar
    {
        public static Key KeyEnum(char c)
        {
            switch (c)
            {
                case '1':
                    return Key.D1;
                case '2':
                    return Key.D2;
                case '3':
                    return Key.D3;
                case '4':
                    return Key.D4;
                case '5':
                    return Key.D5;
                case '6':
                    return Key.D6;
                case '7':
                    return Key.D7;
                case '8':
                    return Key.D8;
                case '9':
                    return Key.D9;
                case '0':
                    return Key.D0;
                case '-':
                    return Key.OemMinus;
                case '=':
                    return Key.OemPlus;
                case 'q':
                case 'й':
                    return Key.Q;
                case 'w':
                case 'ц':
                    return Key.W;
                case 'e':
                case 'у':
                    return Key.E;
                case 'r':
                case 'к':
                    return Key.R;
                case 't':
                case 'е':
                    return Key.T;
                case 'y':
                case 'н':
                    return Key.Y;
                case 'u':
                case 'г':
                    return Key.U;
                case 'i':
                case 'ш':
                    return Key.I;
                case 'o':
                case 'щ':
                    return Key.O;
                case 'p':
                case 'з':
                    return Key.P;
                case '[':
                case 'х':
                    return Key.OemOpenBrackets;
                case ']':
                case 'ъ':
                    return Key.Oem6;
                case 'a':
                case 'ф':
                    return Key.A;
                case 's':
                case 'ы':
                    return Key.S;
                case 'd':
                case 'в':
                    return Key.D;
                case 'f':
                case 'а':
                    return Key.F;
                case 'g':
                case 'п':
                    return Key.G;
                case 'h':
                case 'р':
                    return Key.H;
                case 'j':
                case 'о':
                    return Key.J;
                case 'k':
                case 'л':
                    return Key.K;
                case 'l':
                case 'д':
                    return Key.L;
                case 'z':
                case 'я':
                    return Key.Z;
                case 'x':
                case 'ч':
                    return Key.X;
                case 'c':
                case 'с':
                    return Key.C;
                case 'v':
                case 'м':
                    return Key.V;
                case 'b':
                case 'и':
                    return Key.B;
                case 'n':
                case 'т':
                    return Key.N;
                case 'm':
                case 'ь':
                    return Key.M;
                case ',':
                case 'б':
                    return Key.OemComma;
                case '.':
                case 'ю':
                    return Key.OemPeriod;
                default:
                    return Key.None;
            }
        }
    }
}
