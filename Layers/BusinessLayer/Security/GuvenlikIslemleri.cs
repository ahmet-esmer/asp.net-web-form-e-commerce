using System;


namespace BusinessLayer.Security
{

    public class GuvenlikIslemleri
    {
        public static string hackKontrol(string deger)
        {
            char ciftTirnak = (char)34;
            char slash = (char)92;

            string yenideger = string.Empty;
            yenideger = deger.Replace("'", "");
            yenideger = yenideger.Replace(ciftTirnak.ToString(), "");
            yenideger = yenideger.Replace("<", "");
            yenideger = yenideger.Replace(">", "");
            yenideger = yenideger.Replace(";", "");
            yenideger = yenideger.Replace(slash.ToString(), "");
            //yenideger = yenideger.Replace("select", "");
            //yenideger = yenideger.Replace("insert", "");
            //yenideger = yenideger.Replace("delete", "");
            //yenideger = yenideger.Replace("update", "");
            ////yenideger = yenideger.Replace("or", "");
            //yenideger = yenideger.Replace("and", "");
            //yenideger = yenideger.Replace("where", "");
            //yenideger = yenideger.Replace("from", "");
            //yenideger = yenideger.Replace("union", "");
            //yenideger = yenideger.Replace("join", "");
            //yenideger = yenideger.Replace("inner", "");
            //yenideger = yenideger.Replace("order", "");
            //yenideger = yenideger.Replace("\r\n", "<br />");
            return yenideger.ToString();
        }


        public static string HtmlList(string deger)
        {
            char ciftTirnak = (char)34;

            string yenideger = string.Empty;
            yenideger = deger.Replace("'", "");
            yenideger = yenideger.Replace(ciftTirnak.ToString(), "");
            yenideger = yenideger.Replace("&#32", " ");
            yenideger = yenideger.Replace("&#33", "!");
            yenideger = yenideger.Replace("&#34", " ");
            yenideger = yenideger.Replace("&#35", "#");
            yenideger = yenideger.Replace("&#36", "$");
            yenideger = yenideger.Replace("&#37", "%");
            yenideger = yenideger.Replace("&#38", "&");
            yenideger = yenideger.Replace("&#39", " ");
            yenideger = yenideger.Replace("&#40", "(");
            yenideger = yenideger.Replace("&#41", ")");
            yenideger = yenideger.Replace("&#42", "*");
            yenideger = yenideger.Replace("&#43", "+");
            yenideger = yenideger.Replace("&#44", ",");
            yenideger = yenideger.Replace("&#45", "-");
            yenideger = yenideger.Replace("&#46", ".");
            yenideger = yenideger.Replace("&#47", "/");
            yenideger = yenideger.Replace("&#48", "0");
            yenideger = yenideger.Replace("&#49", "1");
            yenideger = yenideger.Replace("&#50", "2");
            yenideger = yenideger.Replace("&#51", "3");
            yenideger = yenideger.Replace("&#52", "4");
            yenideger = yenideger.Replace("&#53", "5");
            yenideger = yenideger.Replace("&#54", "6");
            yenideger = yenideger.Replace("&#55", "7");
            yenideger = yenideger.Replace("&#56", "8");
            yenideger = yenideger.Replace("&#57", "9");
            yenideger = yenideger.Replace("&#57", ":");
            yenideger = yenideger.Replace("&#59", ";");
            yenideger = yenideger.Replace("&#60", "<");
            yenideger = yenideger.Replace("&#61", "=");
            yenideger = yenideger.Replace("&#62", ">");
            yenideger = yenideger.Replace("&#63", "?");
            yenideger = yenideger.Replace("&#64", "@");
            yenideger = yenideger.Replace("&#65", "A");
            yenideger = yenideger.Replace("&#66", "B");
            yenideger = yenideger.Replace("&#67", "C");
            yenideger = yenideger.Replace("&#68", "D");
            yenideger = yenideger.Replace("&#69", "E");
            yenideger = yenideger.Replace("&#70", "F");
            yenideger = yenideger.Replace("&#71", "G");
            yenideger = yenideger.Replace("&#72", "H");
            yenideger = yenideger.Replace("&#73", "I");
            yenideger = yenideger.Replace("&#74", "J");
            yenideger = yenideger.Replace("&#75", "K");
            yenideger = yenideger.Replace("&#76", "L");
            yenideger = yenideger.Replace("&#77", "M");
            yenideger = yenideger.Replace("&#78", "N");
            yenideger = yenideger.Replace("&#79", "O");
            yenideger = yenideger.Replace("&#80", "P");
            yenideger = yenideger.Replace("&#81", "Q");
            yenideger = yenideger.Replace("&#82", "R");
            yenideger = yenideger.Replace("&#83", "S");
            yenideger = yenideger.Replace("&#84", "T");
            yenideger = yenideger.Replace("&#85", "U");
            yenideger = yenideger.Replace("&#86", "V");
            yenideger = yenideger.Replace("&#87", "W");
            yenideger = yenideger.Replace("&#88", "X");
            yenideger = yenideger.Replace("&#89", "Y");
            yenideger = yenideger.Replace("&#90", "Z");
            yenideger = yenideger.Replace("&#91", "[");
            yenideger = yenideger.Replace("&#92", "\\");
            yenideger = yenideger.Replace("&#93", "]");
            yenideger = yenideger.Replace("&#94", "^");
            yenideger = yenideger.Replace("&#95", "_");
            yenideger = yenideger.Replace("&#96", ",");
            yenideger = yenideger.Replace("&#97", "a");
            yenideger = yenideger.Replace("&#98", "b");
            yenideger = yenideger.Replace("&#99", "c");
            yenideger = yenideger.Replace("&#100", "d");
            yenideger = yenideger.Replace("&#101", "e");
            yenideger = yenideger.Replace("&#102", "f");
            yenideger = yenideger.Replace("&#103", "g");
            yenideger = yenideger.Replace("&#104", "h");
            yenideger = yenideger.Replace("&#105", "i");
            yenideger = yenideger.Replace("&#106", "j");
            yenideger = yenideger.Replace("&#107", "k");
            yenideger = yenideger.Replace("&#108", "l");
            yenideger = yenideger.Replace("&#109", "m");
            yenideger = yenideger.Replace("&#110", "n");
            yenideger = yenideger.Replace("&#111", "o");
            yenideger = yenideger.Replace("&#112", "p");
            yenideger = yenideger.Replace("&#113", "q");
            yenideger = yenideger.Replace("&#114", "r");
            yenideger = yenideger.Replace("&#115", "s");
            yenideger = yenideger.Replace("&#116", "t");
            yenideger = yenideger.Replace("&#117", "u");
            yenideger = yenideger.Replace("&#118", "v");
            yenideger = yenideger.Replace("&#119", "w");
            yenideger = yenideger.Replace("&#120", "x");
            yenideger = yenideger.Replace("&#121", "y");
            yenideger = yenideger.Replace("&#122", "z");
            yenideger = yenideger.Replace("&#123", "}");
            yenideger = yenideger.Replace("&#124", "|");
            yenideger = yenideger.Replace("&#125", "}");
            yenideger = yenideger.Replace("&#126", "~");


            return yenideger.ToString();


            //string yenideger = string.Empty;
            //yenideger = deger.Replace("'", "");
            //yenideger = yenideger.Replace(ciftTirnak.ToString(), "");
            //yenideger = yenideger.Replace("&#32;", " ");
            //yenideger = yenideger.Replace("&#33;", "!");
            //yenideger = yenideger.Replace("&#34;", " ");
            //yenideger = yenideger.Replace("&#35;", "#");
            //yenideger = yenideger.Replace("&#36;", "$");
            //yenideger = yenideger.Replace("&#37;", "%");
            //yenideger = yenideger.Replace("&#38;", "&");
            //yenideger = yenideger.Replace("&#39", " ");
            //yenideger = yenideger.Replace("&#39;", " ");
            //yenideger = yenideger.Replace("&#40", "(");
            //yenideger = yenideger.Replace("&#41", ")");
            //yenideger = yenideger.Replace("&#40;", "(");
            //yenideger = yenideger.Replace("&#41;", ")");
            //yenideger = yenideger.Replace("&#42;", "*");
            //yenideger = yenideger.Replace("&#43;", "+");
            //yenideger = yenideger.Replace("&#44;", ",");
            //yenideger = yenideger.Replace("&#45;", "-");
            //yenideger = yenideger.Replace("&#46;", ".");
            //yenideger = yenideger.Replace("&#47;", "/");
            //yenideger = yenideger.Replace("&#48;", "0");
            //yenideger = yenideger.Replace("&#49;", "1");
            //yenideger = yenideger.Replace("&#50;", "2");
            //yenideger = yenideger.Replace("&#51;", "3");
            //yenideger = yenideger.Replace("&#52;", "4");
            //yenideger = yenideger.Replace("&#53;", "5");
            //yenideger = yenideger.Replace("&#54;", "6");
            //yenideger = yenideger.Replace("&#55;", "7");
            //yenideger = yenideger.Replace("&#56;", "8");
            //yenideger = yenideger.Replace("&#57;", "9");
            //yenideger = yenideger.Replace("&#57;", ":");
            //yenideger = yenideger.Replace("&#59;", ";");
            //yenideger = yenideger.Replace("&#60;", "<");
            //yenideger = yenideger.Replace("&#61;", "=");
            //yenideger = yenideger.Replace("&#62;", ">");
            //yenideger = yenideger.Replace("&#63", "?");
            //yenideger = yenideger.Replace("&#64;", "@");
            //yenideger = yenideger.Replace("&#65;", "A");
            //yenideger = yenideger.Replace("&#66;", "B");
            //yenideger = yenideger.Replace("&#67;", "C");
            //yenideger = yenideger.Replace("&#68;", "D");
            //yenideger = yenideger.Replace("&#69;", "E");
            //yenideger = yenideger.Replace("&#70;", "F");
            //yenideger = yenideger.Replace("&#71;", "G");
            //yenideger = yenideger.Replace("&#72;", "H");
            //yenideger = yenideger.Replace("&#73;", "I");
            //yenideger = yenideger.Replace("&#74;", "J");
            //yenideger = yenideger.Replace("&#75;", "K");
            //yenideger = yenideger.Replace("&#76;", "L");
            //yenideger = yenideger.Replace("&#77;", "M");
            //yenideger = yenideger.Replace("&#78;", "N");
            //yenideger = yenideger.Replace("&#79;", "O");
            //yenideger = yenideger.Replace("&#80", "P");
            //yenideger = yenideger.Replace("&#81;", "Q");
            //yenideger = yenideger.Replace("&#82;", "R");
            //yenideger = yenideger.Replace("&#83;", "S");
            //yenideger = yenideger.Replace("&#84;", "T");
            //yenideger = yenideger.Replace("&#85;", "U");
            //yenideger = yenideger.Replace("&#86;", "V");
            //yenideger = yenideger.Replace("&#87;", "W");
            //yenideger = yenideger.Replace("&#88;", "X");
            //yenideger = yenideger.Replace("&#89;", "Y");
            //yenideger = yenideger.Replace("&#90;", "Z");
            //yenideger = yenideger.Replace("&#91;", "[");
            //yenideger = yenideger.Replace("&#92;", "\\");
            //yenideger = yenideger.Replace("&#93;", "]");
            //yenideger = yenideger.Replace("&#94;", "^");
            //yenideger = yenideger.Replace("&#95;", "_");
            //yenideger = yenideger.Replace("&#96;", ",");
            //yenideger = yenideger.Replace("&#97;", "a");
            //yenideger = yenideger.Replace("&#98;", "b");
            //yenideger = yenideger.Replace("&#99;", "c");
            //yenideger = yenideger.Replace("&#100;", "d");
            //yenideger = yenideger.Replace("&#101;", "e");
            //yenideger = yenideger.Replace("&#102;", "f");
            //yenideger = yenideger.Replace("&#103;", "g");
            //yenideger = yenideger.Replace("&#104;", "h");
            //yenideger = yenideger.Replace("&#105;", "i");
            //yenideger = yenideger.Replace("&#106;", "j");
            //yenideger = yenideger.Replace("&#107;", "k");
            //yenideger = yenideger.Replace("&#108;", "l");
            //yenideger = yenideger.Replace("&#109;", "m");
            //yenideger = yenideger.Replace("&#110;", "n");
            //yenideger = yenideger.Replace("&#111;", "o");
            //yenideger = yenideger.Replace("&#112;", "p");
            //yenideger = yenideger.Replace("&#113;", "q");
            //yenideger = yenideger.Replace("&#114;", "r");
            //yenideger = yenideger.Replace("&#115;", "s");
            //yenideger = yenideger.Replace("&#116;", "t");
            //yenideger = yenideger.Replace("&#117;", "u");
            //yenideger = yenideger.Replace("&#118;", "v");
            //yenideger = yenideger.Replace("&#119;", "w");
            //yenideger = yenideger.Replace("&#120;", "x");
            //yenideger = yenideger.Replace("&#121;", "y");
            //yenideger = yenideger.Replace("&#122;", "z");
            //yenideger = yenideger.Replace("&#123;", "}");
            //yenideger = yenideger.Replace("&#124;", "|");
            //yenideger = yenideger.Replace("&#125;", "}");
            //yenideger = yenideger.Replace("&#126;", "~");
        }


        public static string hackKontrolArama(string deger)
        {
            char ciftTirnak = (char)34;
            char slash = (char)92;

            string yenideger = string.Empty;
            yenideger = deger.Replace("'", "");
            yenideger = yenideger.Replace(ciftTirnak.ToString(), "");
            yenideger = yenideger.Replace("<", "");
            yenideger = yenideger.Replace(">", "");
            yenideger = yenideger.Replace(";", "");
            yenideger = yenideger.Replace("/", "");
            yenideger = yenideger.Replace(slash.ToString(), "");
            yenideger = yenideger.Replace("\r\n", " ");

            return yenideger.ToString();
        }


        public static string htmlDecode(string deger)
        {
            string str_1;

            str_1 = deger.Replace("<", "");
            str_1 = str_1.Replace(">", "");
            str_1 = str_1.Replace("/", "");
            str_1 = str_1.Replace("%", "");
            str_1 = str_1.Replace("<br />", "");

            str_1 = str_1.Replace("td", "");
            str_1 = str_1.Replace("tr", "");
            str_1 = str_1.Replace("span", "");
            str_1 = str_1.Replace("div", "");
            str_1 = str_1.Replace("<p>", "");
            str_1 = str_1.Replace("</p>", "");
            str_1 = str_1.Replace("&nbsp;", " ");
            str_1 = str_1.Replace("&ldquo;", "“");
            str_1 = str_1.Replace("&rdquo;", "”");
            str_1 = str_1.Replace("&rsquo;", "’");
            str_1 = str_1.Replace("&lsquo;", "‘");

            return str_1;
        }


        public static string htmlEncode(string deger)
        {
            string str_2 = string.Empty;

            str_2 = deger.Replace("“", "&ldquo;");
            str_2 = str_2.Replace("”", "&rdquo;");
            str_2 = str_2.Replace("’", "&rsquo;");
            str_2 = str_2.Replace("‘", "&lsquo;");

            return str_2;
        }
    }
}