using System;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Clase05a
{
    public partial class Form1 : Form
    {
        private SqlConnection conexion = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = ISSD-TP2-202302 ; Integrated Security = True;");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string sql = "insert into Materias(descripcion) values (@descripcion)";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox1.Text;
            comando.ExecuteNonQuery();
            conexion.Close();
            textBox1.Text = "";
            MostrarMaterias();
        }

        private string RetornarID()
        {
            string id = "";
            conexion.Open();
            string sql = "select id from Materias where descripcion = @descripcion";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = listBox1.SelectedItem;
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                id = registros["id"].ToString();
            }
            conexion.Close();
            return id;
        }

        private string RetornarDescripcion(int id)
        {
            string descripcion = "";
            conexion.Open();
            string sql = $"select descripcion from Materias where id = {id}";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                descripcion = registros["descripcion"].ToString();
            }
            conexion.Close();
            return descripcion;
        }

        private void MostrarMaterias()
        {
            conexion.Open();
            string sql = "select id, descripcion from Materias";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            listBox1.Items.Clear();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                string id = registros["id"].ToString();
                string descripcion = registros["descripcion"].ToString();
                listBox1.Items.Add(descripcion);
                dataGridView1.Rows.Add(id, descripcion);
            }
            conexion.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarMaterias();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string sql = $"delete from Materias where id = {RetornarID()}";
            conexion.Open();
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
            MostrarMaterias();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string sql = $"update Materias set descripcion = @descripcion where id = {RetornarID()}";
            conexion.Open();
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox1.Text;
            comando.ExecuteNonQuery();
            conexion.Close();
            MostrarMaterias();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
            textBox1.Text = RetornarDescripcion(id);
        }
    }
}