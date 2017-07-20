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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using Gabriel.Cat.Extension;
using Microsoft.Win32;

namespace Etiquetas_Express
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		const string NOMBREARCHIVOCONFIG="etiquetasExpress.config";
		static readonly string PathConfig=System.IO.Path.Combine(Environment.CurrentDirectory,NOMBREARCHIVOCONFIG);
		Etiqueta etiquetaPlantilla;
		
		public Window1()
		{
			XmlDocument xmlConfig;
			etiquetaPlantilla=new Etiqueta();
			
			if(System.IO.File.Exists(PathConfig))
			{
				xmlConfig=new XmlDocument();
				xmlConfig.Load(PathConfig);
				etiquetaPlantilla.PonerPlantilla(xmlConfig.FirstChild);
			}
			InitializeComponent();
			Closing+=GuardarConfiguracion;
		}
		
		public System.Windows.Controls.Primitives.UniformGrid Etiquetas
		{
			get{return this.ugEtiquetas;}
		}

		void GuardarConfiguracion(object sender, System.ComponentModel.CancelEventArgs e)
		{
			XmlDocument xml;
			if(etiquetaPlantilla.Equals(new Etiqueta())){
				if(System.IO.File.Exists(PathConfig))
					System.IO.File.Delete(PathConfig);
			}else{
				xml=new XmlDocument();
				xml.LoadXml(etiquetaPlantilla.GetPlantillaXmlNode().OuterXml);
				xml.Save(PathConfig);
			}
			
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
			ImportarDesdeCsv importarCsv=new ImportarDesdeCsv();
			importarCsv.ShowDialog();
			if(importarCsv.lstEtiquetas.Items.Count>0)
				ugEtiquetas.Children.AddRange(importarCsv.GetEtiquetasCargadas());
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
			const int DPI=96;
			const int MARGEN=270;
			Size pageSize = new Size(8.26 * DPI, 11.69 * DPI); // A4 page, at 96 dpi
			int itemsAdd=0;
			FixedDocument document = new FixedDocument();
			FixedPage fixedPage;
			PageContent pageContent;
			PrintDialog printDialog=new PrintDialog();
			IList<Etiqueta> etiquetas=ugEtiquetas.Children.Casting<Etiqueta>();
			UniformGrid ug;
		
			document.DocumentPaginator.PageSize = pageSize;
			
			do{
				ug=new UniformGrid();
				ug.Columns=2;
				while((ug.Children.Count/2)*etiquetaPlantilla.gEtiqueta.MaxHeight<pageSize.Height-MARGEN&&itemsAdd<etiquetas.Count)
					ug.Children.Add(etiquetas[itemsAdd++].Clone());
				// Create FixedPage
				fixedPage = new FixedPage();
				fixedPage.Width = pageSize.Width;
				fixedPage.Height = pageSize.Height;
				// Add visual, measure/arrange page.
				fixedPage.Children.Add(ug);
				fixedPage.Measure(pageSize);
				fixedPage.Arrange(new Rect(new Point(), pageSize));
				fixedPage.UpdateLayout();

				// Add page to document
				pageContent = new PageContent();				
				((System.Windows.Markup.IAddChild)pageContent).AddChild(fixedPage);
				document.Pages.Add(pageContent);

			}while(itemsAdd<etiquetas.Count);
			// Send to the printer.
			


			
			printDialog.SelectedPagesEnabled=true;
			if(printDialog.ShowDialog().GetValueOrDefault())
			{
				printDialog.PrintDocument(document.DocumentPaginator, "Etiquetas a Imprimir "+DateTime.Now);
				
			}else{
				MessageBox.Show("Se ha cancelado la impresión","Cancelado",MessageBoxButton.OK,MessageBoxImage.Information);
			}
		}
		void MenuEditarEtiqueta_Click(object sender, RoutedEventArgs e)
		{
			//Edita la plantilla
			new EditarPlantilla(){Plantilla=etiquetaPlantilla}.ShowDialog();
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