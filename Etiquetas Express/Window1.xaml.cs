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
		
		void MenuImportar_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void MenuExportar_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void MenuSobre_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
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
			StringBuilder strEspacio1=new StringBuilder(),strEspacio2=new StringBuilder();
			Etiqueta etiquetaNueva=etiquetaPlantilla.GenerarCopia();
			//lo pongo en el medio
			etiquetaNueva.txtBody.Text=strEspacio1.ToString()+txtLinea1.Text+"\n"+strEspacio2.ToString()+txtLinea2.Text;
			ugEtiquetas.Children.Add(etiquetaNueva);
		}
	}
}