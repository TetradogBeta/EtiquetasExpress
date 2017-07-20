/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 20/07/2017
 * Hora: 2:22
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Gabriel.Cat.Extension;
using Microsoft.Win32;

namespace Etiquetas_Express
{
	/// <summary>
	/// Interaction logic for ImportarDesdeCsv.xaml
	/// </summary>
	public partial class ImportarDesdeCsv : Window
	{
		public static char Separador=';';
		string[] articulosAImportar;
		public ImportarDesdeCsv()
		{
			
			OpenFileDialog opn=new OpenFileDialog();
			opn.Filter="Articulos a Importar|*.csv";
			InitializeComponent();
			//cargo el archivo
			if(opn.ShowDialog().GetValueOrDefault())
			{
				//cargo
				articulosAImportar=System.IO.File.ReadAllLines(opn.FileName);
				lstColumnasCodigo.Items.AddRange(articulosAImportar[0].Split(Separador));
				lstColumnasNombreArticulo.Items.AddRange(articulosAImportar[0].Split(Separador));
				lstEtiquetas.Items.AddRange(articulosAImportar.SubList(1));//Quito la metadata
				
				
			}else{
				MessageBox.Show("No se ha seleccionado ningún archivo","Atención",MessageBoxButton.OK,MessageBoxImage.Information);
				this.Close();
			}
			
		}

		public Etiqueta Plantilla {
			get {
				return ePreview;
			}
			set {
				ePreview.PonerPlantilla(value);
			}
		}

		public Etiqueta[] GetEtiquetasCargadas()
		{
			Etiqueta[] etiquetas=new Etiqueta[lstEtiquetas.Items.Count];
			string[] campos;
			StringBuilder strAux=new StringBuilder();
			
			for(int i=0;i<etiquetas.Length;i++)
			{
				etiquetas[i]=new Etiqueta();
				etiquetas[i].PonerPlantilla(Plantilla);
				campos=lstEtiquetas.Items[i].ToString().Split(Separador);
				strAux.Clear();
				for(int j=0;j<lstColumnasCodigo.SelectedItems.Count;j++)
					strAux.Append(campos[lstColumnasCodigo.Items.IndexOf(lstColumnasCodigo.SelectedItems[i])]);
				etiquetas[i].Codigo=strAux.ToString();
				strAux.Clear();
				for(int j=0;j<lstColumnasNombreArticulo.SelectedItems.Count;j++)
					strAux.Append(campos[lstColumnasNombreArticulo.Items.IndexOf(lstColumnasNombreArticulo.SelectedItems[i])]);
				etiquetas[i].Codigo=strAux.ToString();
				
			}
			return etiquetas;
		}
		void Lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			StringBuilder str=new StringBuilder();
			for(int i=0;i<lstColumnasCodigo.SelectedItems.Count;i++)
			{
				str.Append(lstColumnasCodigo.SelectedItems[i].ToString());
			}
			ePreview.Codigo=str.ToString();
			str.Clear();
			for(int i=0;i<lstColumnasNombreArticulo.SelectedItems.Count;i++)
			{
				str.Append(lstColumnasNombreArticulo.SelectedItems[i].ToString());
			}
			ePreview.NombreArticulo=str.ToString();
			
		}
		void MenuEliminarSeleccionados_Click(object sender, RoutedEventArgs e)
		{
			for(int i=0;i<lstEtiquetas.SelectedItems.Count;i++)
				lstEtiquetas.Items.Remove(lstEtiquetas.SelectedItems[i]);
		}

	}
}