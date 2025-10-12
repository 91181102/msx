using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsxChar.Sprites.Models
{
    public class Sprite
    {
        #region: membros da class
        public string Name { get; set; }
        public int Dimension { get; set; }
        public List<SpritePart> Parts { get; set; }
        #endregion

        #region: construtor
        public Sprite()
        {
            this.Name = string.Empty;
            this.Dimension = 0;
            this.Parts = new List<SpritePart>();
            this.CreateParts();
        }

        private void CreateParts()
        {
            SpritePart part;
            for (int i = 0; i < 4; i++)
            {
                part = new SpritePart
                {
                    Index = i
                };
                this.Parts.Add(part);
            }
        }
        #endregion

        public SpritePart GetPart(int index)
        {
            var query = from part in this.Parts
                        where part.Index == index
                        select part;

            if (query.Count() > 0)
            {
                return query.FirstOrDefault();
            }

            return new SpritePart();
        }

        public DataTable GetTable()
        {
            DataTable table = new DataTable();
            for (int i = 0; i < 16; i++)
            {
                table.Columns.Add(i.ToString(), typeof(bool));
            }
            DataRow dr;
            for (int i = 0; i < 16; i++)
            {
                dr = table.NewRow();
                this.FillRow(dr, i);
                table.Rows.Add(dr);
            }
            return table;
        }
    
        private void FillRow(DataRow row, int index)
        {
            var parts = this.JoinParts();
            var part = parts[index];
            var p = string.Empty;

            for (int i = 0; i < 8; i++)
            {
                p = part[i].ToString();
                if (p == "1")
                    row.SetField(i, true);
                else
                    row.SetField(i, false);
            }
        }

        private List<string> JoinParts()
        {
            var parts = new List<string>();
            foreach (var part in this.Parts) 
            {
                foreach (string item in part.GetArray())
                {
                    parts.Add(item);
                }
            }
            return parts;
        }

        /// <summary>
        /// Retorna os dados em decimal.
        /// </summary>
        /// <returns></returns>
        public string GetData()
        {
            string result = string.Empty;
            for (int i = 0; i < 4; i++) { 
                foreach(int part in Parts[i].Bytes)
                {
                    result += part + ",";
                }
            }
            result += ",";

            return result.Replace(",,", string.Empty);
        }
    }

    public partial class SpritePart
    {
        #region: membros da classe
        public int Index { get; set; }
        public List<int> Bytes { get; set; }
        #endregion

        public SpritePart()
        {
            this.Index = 0;
            this.Bytes = new List<int>();
            this.CreateBytes();
        }

        public SpritePart(int index)
        {
            Index = index;
            Bytes = new List<int>();
            this.CreateBytes();
        }

        /// <summary>
        /// Cria um array.
        /// </summary>
        private void CreateBytes()
        {
            if (Bytes != null) 
            { 
                Bytes.Clear();
                for (int i = 0; i < 8; i++)
                    Bytes.Add(255);
            }
        }

        /// <summary>
        /// Altera o valor pelo índice.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetValue(int index, int value)
        {
            Bytes[index] = value;
        }
        
        /// <summary>
        /// Altera o valor pelo índice.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetValue(int index, string value)
        {
            if (index > 7) 
                index = index - 8;
            int i = Convert.ToInt32(value, 2);
            Bytes[index] = i;
        }

        public string GetBinary()
        {
            int toBase = 2;
            string binary = string.Empty;
            string temp = string.Empty;

            foreach (int i in Bytes)
            {
                temp = Convert.ToString(i, toBase);
                if (temp.Length == 1) temp = "0000000" + temp;
                if (temp.Length == 2) temp = "000000" + temp;
                if (temp.Length == 3) temp = "00000" + temp;
                if (temp.Length == 4) temp = "0000" + temp;
                if (temp.Length == 5) temp = "000" + temp;
                if (temp.Length == 6) temp = "00" + temp;
                if (temp.Length == 7) temp = "0" + temp;
                binary += temp + Environment.NewLine;
            }

            return binary;
        }

        public string[] GetArray()
        {
            int toBase = 2;
            List<string> binary = new List<string> ();
            string temp = string.Empty;

            foreach (int i in Bytes)
            {
                temp = Convert.ToString(i, toBase);
                if (temp.Length == 1) temp = "0000000" + temp;
                if (temp.Length == 2) temp = "000000" + temp;
                if (temp.Length == 3) temp = "00000" + temp;
                if (temp.Length == 4) temp = "0000" + temp;
                if (temp.Length == 5) temp = "000" + temp;
                if (temp.Length == 6) temp = "00" + temp;
                if (temp.Length == 7) temp = "0" + temp;
                binary.Add(temp);
            }

            return binary.ToArray();
        }

    }
}
