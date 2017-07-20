/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 19/07/2017
 * Hora: 20:31
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
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		Etiqueta etiquetaPlantilla;
		
		public Window1()
		{
			etiquetaPlantilla=new Etiqueta();
			InitializeComponent();
		}
		
		public System.Windows.Controls.Primitives.UniformGrid Etiquetas
		{
			get{return this.ugEtiquetas;}
		}
		
		void MenuImportarXml_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog opnImportXml=new OpenFileDialog();
			opnImportXml.Filter="Articulos EXPORTADOS|*.xml";
			if(opnImportXml.ShowDialog().GetValueOrDefault())
				ugEtiquetas.Children.AddRange(Etiqueta.ImportarDesdeXml(opnImportXml.FileName));
		}
		void MenuImportarCsv_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void MenuExportarXml_Click(object sender, RoutedEventArgs e)
		{
			const string EXTENSION=".xml";
			string path=EscogerDestinoArchivo(EXTENSION);
			if(path!=null){
				Etiqueta.ExportarXml(etiquetaPlantilla,ugEtiquetas.Children.Casting<Etiqueta>()).Save(path);
			}
		}
		void MenuExportarCsv_Click(object sender, RoutedEventArgs e)
		{
			const string EXTENSION=".csv";
			string path=EscogerDestinoArchivo(EXTENSION);
			if(path!=null){
				System.IO.File.AppendAllText(path,Etiqueta.ExportarCsv(ugEtiquetas.Children.Casting<Etiqueta>()));
			}
		}
		string EscogerDestinoArchivo(string extension)
		{
			SaveFileDialog sfDialog=new SaveFileDialog();
			sfDialog.DefaultExt=extension;
			sfDialog.AddExtension=true;
			string resultado=null;
			if(ugEtiquetas.Children.Count>0){
				if(sfDialog.ShowDialog().GetValueOrDefault())
					resultado=sfDialog.FileName;
			}else{
				MessageBox.Show("No hay etiquetas para exportar!","Atención",MessageBoxButton.OK,MessageBoxImage.Information);
			}
			return resultado;
		}
		void MenuSobre_Click(object sender, RoutedEventArgs e)
		{
			if(MessageBox.Show("Este programa esta hecho para hacer etiquetas rápidamente\nEl programa esta bajo licencia GNU ¿Quiere ver el código fuente?","Sobre la aplicación",MessageBoxButton.YesNo,MessageBoxImage.Question)==MessageBoxResult.Yes)
				System.Diagnostics.Process.Start("https://github.com/TetradogBeta/EtiquetasExpress");
		}
		void MenuImprimir_Click(object sender, RoutedEventArgs e)
		{
			PrintDialog printDialog=new PrintDialog();
			if(printDialog.ShowDialog().GetValueOrDefault())
			{
				printDialog.PrintVisual(ugEtiquetas,"Etiquetas");
				
			}else{
				MessageBox.Show("No se ha cancelado la impresión","Cancelado",MessageBoxButton.OK,MessageBoxImage.Information);
			}
		}
		void MenuEditarEtiqueta_Click(object sender, RoutedEventArgs e)
		{
			//Edita la plantilla
			//pongo la plantilla
			for(int i=0;i<ugEtiquetas.Children.Count;i++)
				((Etiqueta)ugEtiquetas.Children[i]).PonerPlantilla(etiquetaPlantilla);
		}
		void BtnAñadir_Click(object sender, RoutedEventArgs e)
		{
			Etiqueta etiquetaNueva=etiquetaPlantilla.GenerarCopia();
			//lo pongo en el medio
			etiquetaNueva.Codigo=txtLinea1.Text;
			etiquetaNueva.NombreArticulo=txtLinea2.Text;
			ugEtiquetas.Children.Add(etiquetaNueva);
		}
	}
}