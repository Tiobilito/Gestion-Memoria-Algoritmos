using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica_5
{
    public partial class Form1 : Form
    {
        int[] Memoria = { 1000, 400, 1800, 700, 900, 1200, 1500 };
        int[] Puestos = { 0, 0, 0, 0, 0, 0, 0 };
        DataTable Programas = new DataTable();
        int contador = 0;
        int poscision = 0;

        private void Show_Prog()
        {
            int i;
            richTextBox2.Clear();
            richTextBox2.AppendText("Identificador, Nombre, Peso:\n");
            foreach (DataRow row in Programas.Rows)
            {
                string linea = "";
                for (i = 0; i < Programas.Columns.Count; i++)
                {
                    linea += row[i].ToString() + ",\t";
                }
                richTextBox2.AppendText(linea + "\n");
            }
        }

        private void Show_mem()
        {
            int i;
            richTextBox1.Clear();
            richTextBox1.AppendText("Memoria:\n");
            for (i = 0; i < Memoria.Length; i++)
            {
                richTextBox1.AppendText(Memoria[i] + ",");
            }
            richTextBox1.AppendText("\nNum de programa (y posición):\n");
            for (i = 0; i < Memoria.Length; i++)
            {
                richTextBox1.AppendText(Puestos[i] + ",");
            }
        }

        private void Primer_ajuste(int num,int tam)
        {
            int i;
            bool ps = false;
            for (i = 0; i < Memoria.Length; i++)
            {
                if(tam <= Memoria[i] && Puestos[i] == 0)
                {
                    Puestos[i] = num;
                    ps = true;
                    break;
                }
            }

            if (ps == false) MessageBox.Show("No se encontro espacio para el elemento: " + num);

            Show_mem();
        }

        private void Mejor_ajuste(int num, int tam)
        {
            int i;
            int mejor = 0;
            for (i = 0; i < Memoria.Length; i++)
            {
                if (tam <= Memoria[i] && Puestos[i] == 0)
                {
                    if(mejor == 0) mejor = Memoria[i];
                    else if (Memoria[i] >= mejor && mejor >= tam) mejor = Memoria[i];
                }
            }

            for (i = 0; i < Memoria.Length; i++)
            {
                if (Memoria[i] == mejor && Puestos[i] == 0)
                {
                    Puestos[i] = num;
                    break;
                }
            }

            if (mejor == 0) MessageBox.Show("No se encontro espacio para el elemento: " + num);

            Show_mem();
        }

        private void Peor_ajuste(int num, int tam)
        {
            int i;
            int peor = 0;
            for (i = 0; i < Memoria.Length; i++)
            {
                if (tam <= Memoria[i] && Puestos[i] == 0)
                {
                    if (peor == 0) peor = Memoria[i];
                    else if (Memoria[i] <= peor && peor >= tam) peor = Memoria[i];
                }
            }

            for (i = 0; i < Memoria.Length; i++)
            {
                if (Memoria[i] == peor && Puestos[i] == 0)
                {
                    Puestos[i] = num;
                    break;
                }
            }

            if (peor == 0) MessageBox.Show("No se encontro espacio para el elemento: " + num);

            Show_mem();
        }

        private void Siguiente_ajuste(int num, int tam)
        {
            int i, origin = poscision;
            bool ps = false, vl = false;
            if(poscision == Memoria.Length) poscision = 0;
            for (i = poscision; i < Memoria.Length; i++)
            {
                if (vl == true && i == origin)
                {
                    break;
                } else
                {
                    if(poscision == Memoria.Length)
                    {
                        vl = true;
                        poscision = 0;
                    }
                    if (tam <= Memoria[i] && Puestos[i] == 0)
                    {
                        Puestos[i] = num;
                        ps = true;
                        poscision = i;
                        break;
                    }
                } 
            }

            if (ps == false) MessageBox.Show("No se encontro espacio para el elemento: " + num);

            Show_mem();
        }

        public Form1()
        {
            Programas.Columns.Add("Identificador");
            Programas.Columns.Add("Nombres");
            Programas.Columns.Add("Pesos");
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Seleccionar archivo de texto";
            openFileDialog1.Filter = "Archivos de texto (*.txt)|*.txt";
            openFileDialog1.InitialDirectory = @"C:\";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        int index = line.IndexOf('k'); 
                        if (index >= 0) 
                        {
                            line = line.Substring(0, index); 
                        }
                        string[] values = line.Split(',');
                        DataRow row = Programas.NewRow();
                        row["Identificador"] = contador.ToString();
                        row["Nombres"] = values[0];
                        row["Pesos"] = values[1];
                        Programas.Rows.Add(row);
                        contador++;
                    }
                }
                toolStripContainer1.ContentPanel.Enabled = true;
                toolStripButton2.Visible = true;
                toolStripButton3.Visible = true;
                toolStripButton4.Visible = true;
                toolStripButton5.Visible = true;
                toolStripButton6.Visible = true;  
                Show_Prog();
                Show_mem();
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            int i;
            richTextBox1.Clear();
            for (i = 0; i < Puestos.Length; i++)
            {
                Puestos[i] = 0;
            }

            Show_mem();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(richTextBox3.Text) && !String.IsNullOrEmpty(richTextBox4.Text) && comboBox1.SelectedItem != null)
            {
                DataRow row = Programas.NewRow();
                row["Identificador"] = contador.ToString();
                row["Nombres"] = richTextBox3.Text;
                row["Pesos"] = richTextBox4.Text;
                if(comboBox1.SelectedItem.ToString() == "Inicio")
                {
                    Programas.Rows.InsertAt(row, 0);
                } else
                {
                    Programas.Rows.Add(row);
                }
                Show_Prog();
                contador++;
            }
            else
            {
                MessageBox.Show("Rellene todos los campos");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nuevoArreglo;
            if(!String.IsNullOrEmpty(richTextBox5.Text) && comboBox2.SelectedItem != null)
            {
                if (comboBox2.SelectedItem.ToString() == "Inicio")
                {
                    nuevoArreglo = new int[Memoria.Length + 1];
                    nuevoArreglo[0] = int.Parse(richTextBox5.Text);
                    Array.Copy(Memoria, 0, nuevoArreglo, 1, Memoria.Length);
                    Memoria = nuevoArreglo;

                    nuevoArreglo = new int[Memoria.Length + 1];
                    nuevoArreglo[0] = 0;
                    Array.Copy(Puestos, 0, nuevoArreglo, 1, Puestos.Length);
                    Puestos = nuevoArreglo;
                }
                else
                {
                    nuevoArreglo = new int[Memoria.Length + 1];
                    Array.Copy(Memoria, nuevoArreglo, Memoria.Length);
                    nuevoArreglo[nuevoArreglo.Length - 1] = int.Parse(richTextBox5.Text);
                    Memoria = nuevoArreglo;

                    nuevoArreglo = new int[Memoria.Length + 1];
                    Array.Copy(Puestos, nuevoArreglo, Puestos.Length);
                    nuevoArreglo[nuevoArreglo.Length - 1] = 0;
                    Puestos = nuevoArreglo;
                }
                Show_mem();
            } 
            else
            {
                MessageBox.Show("Rellene todos los campos");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            int i;
            for(i = 0; i < Programas.Rows.Count; i++)
            {
                Primer_ajuste(Convert.ToInt32(Programas.Rows[i][0]), Convert.ToInt32(Programas.Rows[i][2]));
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < Programas.Rows.Count; i++)
            {
                Mejor_ajuste(Convert.ToInt32(Programas.Rows[i][0]), Convert.ToInt32(Programas.Rows[i][2]));
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < Programas.Rows.Count; i++)
            {
                Peor_ajuste(Convert.ToInt32(Programas.Rows[i][0]), Convert.ToInt32(Programas.Rows[i][2]));
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < Programas.Rows.Count; i++)
            {
                Siguiente_ajuste(Convert.ToInt32(Programas.Rows[i][0]), Convert.ToInt32(Programas.Rows[i][2]));
            }
        }
    }
}
