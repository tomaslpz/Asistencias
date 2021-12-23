using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto.Logica;
using Proyecto.Datos;

namespace Proyecto.Presentacion
{
    public partial class Personal : UserControl
    {
        public Personal()
        {
            InitializeComponent();
        }
        int Idcargo=0;
        int desde = 1;
        int hasta = 10;
        int contador;
        int Idpersonal;
        private int items_por_pagina = 10;
        string Estado;
        int totalPaginas;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            LocalizarDtvCargos();
            PanelCargos.Visible = false;
            PanelPaginado.Visible = false;
            PanelRegistros.Visible = true;
            PanelRegistros.Dock = DockStyle.Fill;
            btnGuardarPersonal.Visible = true;
            btnGuardarCambiosPersonal.Visible = false;
            Limpiar();
        }
        private void LocalizarDtvCargos()
        {
            dataListadoCargos.Location = new Point(txtSueldoHora.Location.X, txtSueldoHora.Location.Y);
            dataListadoCargos.Size = new Size(469, 141);
            dataListadoCargos.Visible = true;
            lblsueldo.Visible = false;
            PanelBtnGuardarPer.Visible = false;
        }
        private void Limpiar()
        {
            txtNombres.Clear();
            txtIdentificacion.Clear();
            txtCargo.Clear();
            txtSueldoHora.Clear();
            BuscarCargos();
        }

        private void btnGuardarPersonal_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombres.Text))
            {
                if (!string.IsNullOrEmpty(txtIdentificacion.Text))
                {
                    if (!string.IsNullOrEmpty(cbxPais.Text))
                    {
                        if (Idcargo > 0)
                        {
                            if (!string.IsNullOrEmpty(txtSueldoHora.Text))
                            {
                                Insertar_Personal();
                            }
                        }
                    }
                }
            }
        }
        private void Insertar_Personal()
        {
            Lpersonal parametros = new Lpersonal();
            Dpersonal funcion = new Dpersonal();
            parametros.Nombres = txtNombres.Text;
            parametros.Identificacion = txtIdentificacion.Text;
            parametros.Pais = cbxPais.Text;
            parametros.Id_cargo = Idcargo;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoHora.Text);
            //si se registro bien
            if (funcion.InsertarPersonal(parametros) == true)
            {
                ReiniciarPaginado();
                MostrarPersonal();
                PanelRegistros.Visible = false;
            }
        }
        private void MostrarPersonal()
        {
            DataTable dt = new DataTable();
            Dpersonal funcion = new Dpersonal();
            funcion.MostrarPersonal(ref dt, desde, hasta);
            datalistadoPersonal.DataSource = dt;
            DiseñarDtvPersonal();
        }
        private void DiseñarDtvPersonal()
        {
            Bases.DiseñoDtv(ref datalistadoPersonal);
            Bases.DiseñoDtvEliminar(ref datalistadoPersonal);
            PanelPaginado.Visible = true;
            datalistadoPersonal.Columns[2].Visible = false; // oculto id personal
            datalistadoPersonal.Columns[7].Visible = false; //oculto id cargo
        }
        private void InsertarCargos()
        {
            if (!string.IsNullOrEmpty(txtCargoG.Text))
            {
                if (!string.IsNullOrEmpty(txtSueldoG.Text))
                {
                    Lcargos parametros = new Lcargos();
                    Dcargos funcion = new Dcargos();
                    parametros.Cargo = txtCargoG.Text;
                    parametros.SueldoPorHora = Convert.ToDouble(txtSueldoG.Text);
                    if (funcion.InsertarCargo(parametros) == true)
                    {
                        txtCargo.Clear();
                        BuscarCargos();
                        PanelCargos.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Agrege el sueldo", "Falta el Sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Agrege el cargo", "Falta el Cargo", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            
        }
        private void BuscarCargos()
        {
            DataTable dt = new DataTable();
            Dcargos funcion = new Dcargos();
            funcion.BuscarCargos(ref dt, txtCargo.Text);
            dataListadoCargos.DataSource = dt;
            Bases.DiseñoDtv(ref dataListadoCargos);
            dataListadoCargos.Columns[1].Visible = false;
            dataListadoCargos.Columns[3].Visible = false;
            dataListadoCargos.Visible = true;
        }

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            BuscarCargos();
        }

        private void btnAgregarCargo_Click(object sender, EventArgs e)
        {
            PanelCargos.Visible = true;
            PanelCargos.Dock = DockStyle.Fill;
            PanelCargos.BringToFront();
            btnGuardarC.Visible = true;
            btnGuardarCambiosC.Visible = false;
            txtCargo.Clear();
            txtSueldoG.Clear();
        }

        private void btnGuardarC_Click(object sender, EventArgs e)
        {
            InsertarCargos();
        }

        private void txtSueldoG_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoG, e);
        }

        private void txtSueldoHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoHora, e);
        }

        private void dataListadoCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListadoCargos.Columns["EditarC"].Index)
            {
                ObtenerCargoEditar();
            }
            if (e.ColumnIndex == dataListadoCargos.Columns["Cargo"].Index)
            {
                ObtenerDatosCargos();
            }
        }
        private void ObtenerDatosCargos()
        {
            Idcargo = Convert.ToInt32(dataListadoCargos.SelectedCells[1].Value);
            txtCargo.Text = dataListadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoHora.Text = dataListadoCargos.SelectedCells[3].Value.ToString();
            dataListadoCargos.Visible = false;
            PanelBtnGuardarPer.Visible = true;
            lblsueldo.Visible = true;
        }
        private void ObtenerCargoEditar()
        {
            Idcargo = Convert.ToInt32(dataListadoCargos.SelectedCells[1].Value);
            txtCargoG.Text = dataListadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoG.Text = dataListadoCargos.SelectedCells[3].Value.ToString();
            btnGuardarC.Visible = false;
            btnGuardarCambiosC.Visible = true;
            txtCargoG.Focus(); //posiciono el cursor en cargosG
            txtCargoG.SelectAll();
            PanelCargos.Visible = true;
            PanelCargos.Dock = DockStyle.Fill;
            PanelCargos.BringToFront();
        }

        private void BtnVolverCargo_Click(object sender, EventArgs e)
        {
            PanelCargos.Visible = false;
        }

        private void BtnVolverPersonal_Click(object sender, EventArgs e)
        {
            PanelRegistros.Visible = false;
        }

        private void btnGuardarCambiosC_Click(object sender, EventArgs e)
        {
            EditarCargos();
        }
        private void EditarCargos()
        {
            Lcargos parametros = new Lcargos();
            Dcargos funcion = new Dcargos();
            parametros.Id_cargo = Idcargo;
            parametros.Cargo = txtCargoG.Text;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoG.Text);
            if (funcion.editarCargo(parametros) == true)
            {
                txtCargo.Clear();
                BuscarCargos();
                PanelCargos.Visible = false;
            }
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            MostrarPersonal();
            ReiniciarPaginado();
        }

        private void ReiniciarPaginado()
        {
            desde = 1;
            hasta = 10;
            Contar();
            if (contador > hasta)
            {
                btnSig.Visible = true;
                btnAnt.Visible = false;
                btnPrimera.Visible = true;
                btnUltima.Visible = true;
            }
            else
            {
                btnSig.Visible = false;
                btnAnt.Visible = false;
                btnPrimera.Visible = false;
                btnUltima.Visible = false;
            }
            Paginar();
        }

        private void datalistadoPersonal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistadoPersonal.Columns["Eliminar"].Index)
            {
                DialogResult result = MessageBox.Show("Solo se cambiara el estado para que no pueda acceder, ¿desea continuar?","Eliminando registros",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if ( result == DialogResult.OK)
                {
                    EliminarPersonal();
                }              
            }
            if (e.ColumnIndex == datalistadoPersonal.Columns["Editar"].Index)
            {
                ObtenerDatos();
            }

        }
        private void ObtenerDatos()
        {
            Idpersonal = Convert.ToInt32(datalistadoPersonal.SelectedCells[2].Value);
            Estado = datalistadoPersonal.SelectedCells[8].Value.ToString();
            if (Estado == "ELIMINADO")
            {
                restaurar_personal();
            }
            else
            {
                LocalizarDtvCargos();
                txtNombres.Text = datalistadoPersonal.SelectedCells[3].Value.ToString();
                txtIdentificacion.Text =  datalistadoPersonal.SelectedCells[4].Value.ToString();
                cbxPais.Text = datalistadoPersonal.SelectedCells[10].Value.ToString();
                txtCargo.Text = datalistadoPersonal.SelectedCells[6].Value.ToString();
                Idcargo = Convert.ToInt32(datalistadoPersonal.SelectedCells[7].Value);
                txtSueldoHora.Text = datalistadoPersonal.SelectedCells[5].Value.ToString();
                PanelPaginado.Visible = false;
                PanelRegistros.Visible = true;
                PanelRegistros.Dock = DockStyle.Fill;
                dataListadoCargos.Visible = false;
                lblsueldo.Visible = true;
                PanelBtnGuardarPer.Visible = true;
                btnGuardarCambiosC.Visible = true;
                btnGuardarPersonal.Visible = false;
                PanelCargos.Visible = false;
            }
        }
        private void restaurar_personal()
        {
            DialogResult result = MessageBox.Show("Este personal se elimino, ¿Desea volver a habilitarlo?", "Restauracion de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                HabilitarPersonal();
            }
        }
        private void HabilitarPersonal()
        {
            Lpersonal parametros = new Lpersonal();
            Dpersonal funcion = new Dpersonal();
            parametros.Id_personal = Idpersonal;
            if (funcion.restaurar_personal(parametros) == true)
            {
                MostrarPersonal();
            }
        }
        private void EliminarPersonal()
        {
            Idpersonal = Convert.ToInt32(datalistadoPersonal.SelectedCells[2].Value);
            Lpersonal parametros = new Lpersonal();
            Dpersonal funcion = new Dpersonal();
            parametros.Id_personal = Idpersonal;
            if (funcion.eliminarPersonal(parametros) == true)
            {
                MostrarPersonal();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DiseñarDtvPersonal();
            timer1.Stop();
        }

        private void btnGuardarCambiosPersonal_Click(object sender, EventArgs e)
        {
            EditarPersonal();
        }

        private void EditarPersonal()
        {
            Lpersonal parametros = new Lpersonal();
            Dpersonal funcion = new Dpersonal();
            parametros.Id_personal = Idpersonal;
            parametros.Nombres = txtNombres.Text;
            parametros.Identificacion = txtIdentificacion.Text;
            parametros.Pais = cbxPais.Text;
            parametros.Id_cargo = Idcargo;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoHora.Text);
            if (funcion.editarPersonal(parametros) == true)
            {
                MostrarPersonal();
                PanelRegistros.Visible = false;
            }
        }
        //boton siguiente
        private void button7_Click(object sender, EventArgs e)
        {
            desde += 10;
            hasta += 10;
            MostrarPersonal();
            Contar();
            if (contador > hasta)
            {
                btnSig.Visible = true;
                btnAnt.Visible = true;
            }
            else
            {
                btnSig.Visible = false;
                btnAnt.Visible = true;
            }
            Paginar();
        }
        
        private void Paginar()
        {
            try
            {
                lblPagina.Text = (hasta / items_por_pagina).ToString();
                lblTotalPaginas.Text = Math.Ceiling(Convert.ToSingle(contador) / items_por_pagina).ToString(); //Me quedo con el valor entero
                totalPaginas = Convert.ToInt32(lblTotalPaginas.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Contar()
        {
            Dpersonal funcion = new Dpersonal();
            funcion.ContarPersonal(ref contador);
        }

        private void btnAnt_Click(object sender, EventArgs e)
        {
            desde -= 10;
            hasta -= 10;
            MostrarPersonal();
            Contar();
            if (contador > hasta)
            {
                btnSig.Visible = true;
                btnAnt.Visible = true;
            }
            else
            {
                btnSig.Visible = false;
                btnAnt.Visible = true;
            }
            if (desde == 1)
            {
                ReiniciarPaginado();
            }
            Paginar();
        }

        private void btnUltima_Click(object sender, EventArgs e)
        {
            hasta = totalPaginas * items_por_pagina;
            desde = hasta - 9;
            MostrarPersonal();
            Contar();
            if (contador > hasta)
            {
                btnSig.Visible = true;
                btnAnt.Visible = true;
            }
            else
            {
                btnSig.Visible = false;
                btnAnt.Visible = true;
            }
            Paginar();
        }

        private void btnPrimera_Click(object sender, EventArgs e)
        {
            ReiniciarPaginado();
            MostrarPersonal();
        }
    }
}
