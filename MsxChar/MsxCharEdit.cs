using MsxChar.Sprites.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MsxChar
{
    public partial class MsxCharEdit : Form
    {
        #region: objetos do formulário
        private List<Form> Janelas = new List<Form>();
        #endregion

        #region: construtor
        public MsxCharEdit()
        {
            InitializeComponent();
            this.LoadEvents();
        }

        private void LoadEvents()
        {
            this.Mni_Sprite_Novo.Click += new EventHandler(this.Mni_Sprite_Novo_Click);
        }
        #endregion

        #region: eventos de menu
        private void Mni_Sprite_Novo_Click(object sender, EventArgs e)
        {
            var Form = new Form_Sprites();
            Janelas.Add(Form);
            Form.SetFormIndex(Form);
            Form.Show();
        }
        #endregion
    }
}
