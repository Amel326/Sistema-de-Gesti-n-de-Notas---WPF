using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using System.Windows.Media.Media3D;

namespace CREAR_ARCHIVO
{
    
    public partial class MainWindow : Window
    {
        string ruta = @"D:\C SHARP\ARCHIVOS\archivo.txt";
        StreamReader sr;
        StreamWriter sw;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btncrear_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Archivo de Texto|*.txt";
            dialog.ShowDialog();

            ruta = dialog.FileName;
            lblinfo.Content = ruta;

            try
            {
                sw = File.CreateText(ruta);
                MessageBox.Show("Archivo creado");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                sw.Close();
            }
        }

        private void btnabrir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Archivo de texto|*.txt";
            dialog.ShowDialog();
            ruta = dialog.FileName;
            lblinfo.Content = ruta;
        }

        private void btnmostrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lbxdatos.Items.Clear();
                sr = new StreamReader(ruta);

                string linea = sr.ReadLine();
                while (linea != null)
                {
                    lbxdatos.Items.Add(linea);
                    linea = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally { sr.Close(); }
        }

        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sw = File.AppendText(ruta);
                sw.WriteLine(txtnombre.Text+"|"+txtnota.Text+"|"+txtmateria.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally { sw.Close(); }
        }

        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = Interaction.InputBox("Ingrese nombre del estudiante:");
            string nom = "", materia = "";
            int nota = 0;

            bool encontrado = false;

            try
            {
                sr = new StreamReader(ruta);
                string linea = sr.ReadLine();
                while (linea != null) 
                {
                    string[] arreglo = linea.Split('|');
                    if (nombre == arreglo[0])
                    {
                        encontrado = true;
                        nom = arreglo[0];
                        nota = int.Parse(arreglo[1]);
                        materia = arreglo[2];

                        break;
                    }

                    linea = sr.ReadLine();
                }
                if (encontrado)
                {
                    MessageBox.Show("Estudiante encontrado ->\n"+
                        "Nombre: "+nom+" Nota: "+nota+" materia: "+materia);
                }
                else
                {
                    MessageBox.Show(nombre + " el estudiante no encontrado");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally { sr.Close(); }
        }

        private void btnmayor_Click(object sender, RoutedEventArgs e)
        {
            string aux = "";
            int nota = -100;
            string[] arreglo;
            try
            {
                sr = new StreamReader(ruta);
                string linea = sr.ReadLine();
                while (linea != null)
                {
                    arreglo = linea.Split('|');
                    if (int.Parse(arreglo[1]) > nota)
                    {
                        nota = int.Parse(arreglo[1]);
                        aux = linea;
                    }

                    linea = sr.ReadLine();
                }
                arreglo = aux.Split('|');
                MessageBox.Show("Estudiante encontrado ->\n" +
                        "Nombre: " + arreglo[0] + " Nota: " + arreglo[1] + " materia: " + arreglo[2]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { sr.Close(); }
        }

        private void btnaprobados_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lbxdatos.Items.Clear();
                sr = new StreamReader(ruta);

                string linea = sr.ReadLine();
                while (linea != null)
                {
                    string[] arreglo = linea.Split('|'); 
                    if (int.Parse(arreglo[1])>=61)
                    {
                        lbxdatos.Items.Add(linea);
                    }
                    linea = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally { sr.Close(); }
        }

        private void btnreprobados_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lbxdatos.Items.Clear();
                sr = new StreamReader(ruta);

                string linea = sr.ReadLine();
                while (linea != null)
                {
                    string[] arreglo = linea.Split('|');
                    if (int.Parse(arreglo[1]) < 61)
                    {
                        lbxdatos.Items.Add(linea);
                    }
                    linea = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally { sr.Close(); }
        }

        private void btnpromedio_Click(object sender, RoutedEventArgs e)
        {
            double promedio = 0;
            int suma = 0;
            int cont = 0;
            try
            {
                sr = new StreamReader(ruta);

                string linea = sr.ReadLine();
                while (linea != null)
                {
                    string[] arreglo = linea.Split('|');
                    cont++;
                    suma += int.Parse(arreglo[1]);
                    linea = sr.ReadLine();
                }

                promedio = suma/ cont;
                MessageBox.Show("El promedio de las notas es: " + promedio);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally { sr.Close(); }
        }

        private void btnpocentaje_Click(object sender, RoutedEventArgs e)
        {
            double porcentajeapro = 0, porcentajerepro = 0;
            int aprodados = 0, reprobados = 0, cont=0;
            try
            {
                sr = new StreamReader(ruta);

                string linea = sr.ReadLine();
                while (linea != null)
                {
                    string[] arreglo = linea.Split('|');
                    cont++;
                    if (int.Parse(arreglo[1]) >= 61)
                    {
                        aprodados++;
                    }
                    else
                    {
                        reprobados++;
                    }
                    linea = sr.ReadLine();
                }
                porcentajeapro = (aprodados * 100) / cont;
                porcentajerepro = (reprobados * 100) / cont;
                MessageBox.Show("El porcentaje de aprobados es: " + porcentajeapro+"%"+
                    "\nde reprobados es: "+porcentajerepro+"%");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally { sr.Close(); }
        }

        
    }
}
