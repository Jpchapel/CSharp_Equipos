using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PorEquipos
{
    public partial class f_Cros : Form
    {

        string[,] aEquipos = new string[4, 3] { { "12","UCAM","0"},
                                               { "15","USC","0"},
                                               { "20","UNIOVI","0"},
                                               { "83","UNILEON","0"}};

        string[,] aCorredores = new string[8, 3] { {"4", "Juan Bueno", "12" },
                                                   {"5", "María García", "12" },
                                                   {"6", "Begoña Arce", "15" },
                                                   {"7", "Valentín Roca", "15" },
                                                   {"8", "Vicente Fraga", "20" },
                                                   {"9", "Marisol Solete", "20" },
                                                   {"10", "Aparicio Vera", "83" },
                                                   {"11", "Marta Méndez", "83" }};


        int[] aCarreira = new int[8];


        public f_Cros()
        {
            InitializeComponent();
        }

        private void f_Cros_Load(object sender, EventArgs e)
        {
            cargarEquipos();
            cargarCorredores();

            btIniciar.Enabled = true;
        }                

        private void btIniciar_Click(object sender, EventArgs e)
        {
            sortearCarrera();
            mostrarResultadoCarrera();
            cargarPodio();
            btIniciar.Enabled = false;
        }

        private void cargarEquipos()
        {
            for (int equipo = 0; equipo < aEquipos.GetLength(0); equipo++)
            {
                lbxEquipos.Items.Add(aEquipos[equipo, 0] + " \t-\t " + aEquipos[equipo, 1]);
            }
        }

        private void cargarCorredores()
        {
            for (int corredor = 0; corredor < aCorredores.GetLength(0); corredor++)
            {
                lbxCorredores.Items.Add(aCorredores[corredor, 0] + " \t-\t " + aCorredores[corredor, 1]);
            }
        }

        private void sortearCarrera()
        {
            Random r = new Random();
            int dorsal;

            bool Existe;
            for (int carrera = 0; carrera < aCarreira.GetLength(0); carrera++)
            {
                dorsal = r.Next(4, 12);
                Existe = false;
                for (int j = 0; j < carrera; j++)
                {
                    if (aCarreira[j] == dorsal)
                        Existe = true;
                }
                if (Existe)
                    carrera -= 1;
                else
                    aCarreira[carrera] = dorsal;
            }   
        }

        private void mostrarResultadoCarrera()
        {
            for (int carrera = 0; carrera < aCarreira.Length; carrera++)
            {
                for (int corredor = 0; corredor < aCorredores.GetLength(0); corredor++)
                {
                    if (aCarreira[carrera] == Convert.ToInt32(aCorredores[corredor, 0]))
                    {
                        for (int equipo = 0; equipo < aEquipos.GetLength(0); equipo++)
                        {
                            if (aCorredores[corredor, 2].Equals(aEquipos[equipo, 0]))
                            {
                                lbxResultado.Items.Add(aCarreira[carrera] + "\t" + aCorredores[corredor, 1] + "\t" + aEquipos[equipo, 1]);
                                aEquipos[equipo, 2] = Convert.ToString(Convert.ToInt32(aEquipos[equipo, 2]) + (Convert.ToInt32(aCarreira.Length) - carrera));
                                break;
                            }
                        }
                        break;
                    }
                }
            }

            lbxResultado.Items.Add("\n");
            lbxResultado.Items.Add("\n");
            cargarResultadosEquipos();
        }

        private void cargarResultadosEquipos()
        {
            for (int equipo = 0; equipo < aEquipos.GetLength(0); equipo++)
            {
                lbxResultado.Items.Add(aEquipos[equipo, 1] + "\t\t\t " + aEquipos[equipo, 2]);
            }
        }

        private void cargarPodio()
        {
            string[] aPodio = new string[3];
            ComprobarTresPrimeros(aPodio);

            MostrarPodio(aPodio);
        }

        private void ComprobarTresPrimeros(string[] aPodio)
        {
            for (int i = 0; i < 3; i++)
            {
                int indiceMayor = 0;
                int mayor = 0;

                for (int equipo = 0; equipo < aEquipos.GetLength(0); equipo++)
                {
                    if (Convert.ToInt32(aEquipos[equipo, 2]) > mayor)
                    {
                        bool comprobador = false;

                        for (int k = 0; k < i; k++)
                        {
                            if (Convert.ToString(equipo) == aPodio[k])
                            {
                                comprobador = true;
                            }
                        }
                        if (!comprobador)
                        {
                            mayor = Convert.ToInt32(aEquipos[equipo, 2]);
                            indiceMayor = equipo;
                        }
                    }
                }

                aPodio[i] = Convert.ToString(indiceMayor);
            }
        }

        private void MostrarPodio(string[] aPodio)
        {
            foreach (Control elemento in gbxPodium.Controls)
            {
                if (elemento.Name.StartsWith("lblPodium"))
                {
                    elemento.Text = aEquipos[Convert.ToInt32(aPodio[Convert.ToInt32(elemento.Name.Substring(9, 1)) - 1]), 1];
                }
            }
        }
    }
}

