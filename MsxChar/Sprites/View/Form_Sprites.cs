using MsxChar.Sprites.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MsxChar.Sprites.View
{
    public partial class Form_Sprites : Form
    {
        #region: objetos do formulario
        private Sprite _sprite;
        private Form FormIndex;
        #endregion

        #region: construtor
        public Form_Sprites()
        {
            InitializeComponent();
            this._sprite = new Sprite();
            this.SetEvents();
            this.SetGrid();
        }

        private void SetEvents()
        {
            this.BtnSair.Click += new EventHandler(this.BtnSair_Click);
        }

        private void SetGrid()
        {
            this.Dg_a1.DataSource = this._sprite.GetTable();
            this.FormatGrid(this.Dg_a1);            
        }

        private void FormatGrid(DataGridView dg)
        {
            for(int i=0; i<16;i++)
                dg.Columns[i].Width = 20;
            dg.MultiSelect = false;
            dg.AllowUserToAddRows = false;
            dg.AllowUserToDeleteRows = false;
            
        }

        public void SetFormIndex(Form form)
        {
            this.FormIndex = form;
        }
        #endregion

        #region:eventos
        private void BtnSair_Click(object sender, EventArgs e)
        {           
            FormIndex.Close();
            FormIndex.Dispose();
        }
        #endregion

        private void Dg_a1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            bool value = false;

            if (!Dg_a1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals(DBNull.Value))
                value = (bool)Dg_a1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            if (value) 
            {
                Dg_a1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                value = false;
            }
            else
            {
                Dg_a1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Gray;
                value = true;
            }
            //MessageBox.Show("Col: " + e.ColumnIndex.ToString() + " valor: " + value.ToString());

            this.Change(e.RowIndex, e.ColumnIndex, value);
        }

        private void Change(int row, int col, bool cell_value)
        {
            bool value;
            string bytes = string.Empty;
            int part = 0;
            int start = 0;

            if (col < 8 && row < 8) { part = 0; start = 0; }
            if (col < 8 && row > 7) { part = 1; start = 0; }
            if (col > 7 && row < 8) { part = 2; start = 8; }
            if (col > 7 && row > 7) { part = 3; start = 8; }

            for (int i = start; i < start + 8; i++)
            {
                value = false;
                if (i == col)
                    {
                        value = cell_value;
                    }                
                else
                    {
                        if (!Dg_a1.Rows[row].Cells[i].Value.Equals(DBNull.Value))
                            value = (bool)Dg_a1.Rows[row].Cells[i].Value;
                    }
                                
                if (value)
                    {
                        bytes += "1";
                    }                
                else
                    {
                        bytes += "0";
                    }
            }
            //if (row > 7) row = row - 8;
            _sprite.Parts[part].SetValue(row, bytes);
            this.Txt_a0.Text = _sprite.Parts[0].GetBinary();
            this.Txt_a1.Text = _sprite.Parts[1].GetBinary();
            this.Txt_a2.Text = _sprite.Parts[2].GetBinary();
            this.Txt_a3.Text = _sprite.Parts[3].GetBinary();
            this.Txt_decimal.Text = _sprite.GetData();
        }

        private void Dg_a1_Sorted(object sender, EventArgs e)
        {

        }

        private void Chk_espelhar_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
