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
        int contador = 1;
        int poscision = 0;

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

            if (ps == false) MessageBox.Show("No se encontro espacio");

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

            if (mejor == 0) MessageBox.Show("No se encontro espacio");

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

            if (peor == 0) MessageBox.Show("No se encontro espacio");

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

            if (ps == false) MessageBox.Show("No se encontro espacio");

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

        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] == Column3)
            {
                richTextBox1.Clear();
                Primer_ajuste(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value),
                              Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value));
            } 
            if (dataGridView1.Columns[e.ColumnIndex] == Column4)
            {
                richTextBox1.Clear();
                Mejor_ajuste(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value),
                             Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value));
            } 
            if (dataGridView1.Columns[e.ColumnIndex] == Column5)
            {
                richTextBox1.Clear();
                Peor_ajuste(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value),
                            Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value));
            } 
            if (dataGridView1.Columns[e.ColumnIndex] == Column6)
            {
                richTextBox1.Clear();
                Siguiente_ajuste(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value),
                                 Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value));
            }

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
                        string row = contador.ToString() + "," + line; 
                        dataGridView1.Rows.Add(row.Split(',')); 
                        contador++; 
                    }
                }
                toolStripContainer1.ContentPanel.Enabled = true;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string row = contador.ToString();
            dataGridView1.Rows.Add(row);
            contador++;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int i;
            richTextBox1.Clear();
            for (i = 0;i<Puestos.Length;i++)
            {
                Puestos[i] = 0;
            }

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
    }
}
