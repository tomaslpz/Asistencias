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
            Listado.BackgroundColor = Color.FromArgb(29,29,29);
            Listado.EnableHeadersVisualStyles = false; //deshabilita el estilo que viene por default
            Listado.BorderStyle = BorderStyle.None; //quitado de bordes
            Listado.CellBorderStyle = DataGridViewCellBorderStyle.None;
            Listado.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            Listado.RowHeadersVisible = false; //quito la columna mas a la izquierda
            DataGridViewCellStyle cabecera = new DataGridViewCellStyle();
            cabecera.BackColor = Color.FromArgb(29, 29, 29);
            cabecera.ForeColor = Color.White;
            cabecera.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            Listado.ColumnHeadersDefaultCellStyle = cabecera;
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
