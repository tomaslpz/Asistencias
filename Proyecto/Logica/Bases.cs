using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Proyecto.Logica
{
    public class Bases
    {
        public static void DiseñoDtv(ref DataGridView Listado)
        {
            Listado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Listado.BackgroundColor = Color.Red;
        }
        public static object Decimales(TextBox CajaTexto, KeyPressEventArgs e)
        {
            //Si escribo una coma que lo convierta a punto
            if ((e.KeyChar=='.') || (e.KeyChar==','))
            {
                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            //Si ya existe un punto, bloquea la escritura
            else if (e.KeyChar == '.' && (~CajaTexto.Text.IndexOf('.')) != 0)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',')
            {
                e.Handled = false;
            }
            //Para cualquier otro caso, aplicar bloqueo
            else
            {
                e.Handled = true;
            }

            return null;
        }
    }
}
