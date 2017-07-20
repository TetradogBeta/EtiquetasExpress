/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 07/19/2017
 * Hora: 20:52
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
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace Etiquetas_Express
{
	/// <summary>
	/// Interaction logic for Etiqueta.xaml
	/// </summary>
	public partial class Etiqueta : UserControl
	{
		public enum ArticuloXml
		{
			Codigo,
			NombreArticulo
		}
		public enum PlantillaXml
		{
			Width,
			Height,
			BackGroundBorder,
			BackGroundText
		}
		public Etiqueta()
		{
			ContextMenu menu=new ContextMenu();
			MenuItem item=new MenuItem();
			
			item.Header="Eliminar";
			item.Click+=(s,e)=>((UniformGrid)Parent).Children.Remove(this);
			menu.Items.Add(item);
			ContextMenu=menu;
			
			InitializeComponent();
		}
		public Etiqueta(XmlNode nodoEtiqueta):this()
		{
			Codigo=nodoEtiqueta.ChildNodes[(int)ArticuloXml.Codigo].InnerText.DescaparCaracteresXML();
			NombreArticulo=nodoEtiqueta.ChildNodes[(int)ArticuloXml.NombreArticulo].InnerText.DescaparCaracteresXML();
		}
		public string Codigo
		{
			get{return txtCodigo.Text;}
			set{txtCodigo.Text=value;}
		}
		public string NombreArticulo
		{
			get{return txtBody.Text;}
			set{txtBody.Text=value;}
		}
		public Etiqueta GenerarCopia()
		{
			RowDefinition row;
			ColumnDefinition column;
			Etiqueta etiqueta=new Etiqueta();
			
			etiqueta.gEtiqueta.Height=gEtiqueta.Height;
			etiqueta.gEtiqueta.Width=gEtiqueta.Width;
			
			etiqueta.gEtiqueta.ColumnDefinitions.Clear();
			for(int i=0;i<gEtiqueta.ColumnDefinitions.Count;i++){
				column=new ColumnDefinition();
				column.Width=gEtiqueta.ColumnDefinitions[i].Width;
				etiqueta.gEtiqueta.ColumnDefinitions.Add(column);
			}
			
			etiqueta.gEtiqueta.RowDefinitions.Clear();
			for(int i=0;i<gEtiqueta.RowDefinitions.Count;i++){
				row=new RowDefinition();
				row.Height=gEtiqueta.RowDefinitions[i].Height;
				etiqueta.gEtiqueta.RowDefinitions.Add(row);
			}
			etiqueta.Background=Background;
			etiqueta.txtBody.Background=txtBody.Background;
			return etiqueta;
		}

		public void PonerPlantilla(XmlNode firstChild)
		{
			byte[] aux;
			
			gEtiqueta.MinHeight=double.Parse(firstChild.ChildNodes[(int)PlantillaXml.Height].InnerText);
			gEtiqueta.MaxHeight=double.Parse(firstChild.ChildNodes[(int)PlantillaXml.Height].InnerText);
			gEtiqueta.MinWidth=double.Parse(firstChild.ChildNodes[(int)PlantillaXml.Width].InnerText);
			gEtiqueta.MaxWidth=double.Parse(firstChild.ChildNodes[(int)PlantillaXml.Width].InnerText);
			aux=Serializar.GetBytes(int.Parse(firstChild.ChildNodes[(int)PlantillaXml.BackGroundBorder].InnerText));
			gEtiqueta.Background=new SolidColorBrush(Color.FromArgb(aux[0],aux[1],aux[2],aux[3]));
			aux=Serializar.GetBytes(int.Parse(firstChild.ChildNodes[(int)PlantillaXml.BackGroundText].InnerText));
			gCodigo.Background=new SolidColorBrush(Color.FromArgb(aux[0],aux[1],aux[2],aux[3]));
			gBody.Background=new SolidColorBrush(Color.FromArgb(aux[0],aux[1],aux[2],aux[3]));
		}

		public void PonerPlantilla(Etiqueta etiqueta)
		{
			RowDefinition row;
			ColumnDefinition column;
			
			gEtiqueta.MinHeight=etiqueta.gEtiqueta.MinHeight;
			gEtiqueta.MaxHeight=etiqueta.gEtiqueta.MaxHeight;
			
			gEtiqueta.MinWidth=etiqueta.gEtiqueta.MinWidth;
			gEtiqueta.MaxWidth=etiqueta.gEtiqueta.MaxWidth;
			
			gEtiqueta.ColumnDefinitions.Clear();
			for(int i=0;i<etiqueta.gEtiqueta.ColumnDefinitions.Count;i++)
			{
				column=new ColumnDefinition();
				column.Width=etiqueta.gEtiqueta.ColumnDefinitions[i].Width;
				gEtiqueta.ColumnDefinitions.Add(column);
			}
			
			gEtiqueta.RowDefinitions.Clear();
			for(int i=0;i<etiqueta.gEtiqueta.RowDefinitions.Count;i++)
			{
				row=new RowDefinition();
				row.Height=etiqueta.gEtiqueta.RowDefinitions[i].Height;
				gEtiqueta.RowDefinitions.Add(row);
			}
			
			
			Background=etiqueta.Background;
			txtBody.Background=etiqueta.txtBody.Background;
			
		}
		public XmlNode ToXmlNode()
		{
			StringBuilder strNodo=new StringBuilder();
			XmlDocument xml=new XmlDocument();
			strNodo.Append("<Etiqueta>");
			strNodo.Append("<");
			strNodo.Append(ArticuloXml.Codigo.ToString());
			strNodo.Append(">");
			strNodo.Append(Codigo.EscaparCaracteresXML());
			strNodo.Append("</");
			strNodo.Append(ArticuloXml.Codigo.ToString());
			strNodo.Append(">");
			
			strNodo.Append("<");
			strNodo.Append(ArticuloXml.NombreArticulo.ToString());
			strNodo.Append(">");
			strNodo.Append(NombreArticulo.EscaparCaracteresXML());
			strNodo.Append("</");
			strNodo.Append(ArticuloXml.NombreArticulo.ToString());
			strNodo.Append(">");
			strNodo.Append("</Etiqueta>");
			xml.LoadXml(strNodo.ToString());
			return xml.FirstChild;
		}
		public XmlNode GetPlantillaXmlNode()
		{
			StringBuilder strNodo=new StringBuilder();
			XmlDocument xml=new XmlDocument();
			strNodo.Append("<PlantillaEtiqueta>");
			strNodo.Append("<");
			strNodo.Append(PlantillaXml.Width.ToString());
			strNodo.Append(">");
			strNodo.Append(gEtiqueta.MaxWidth);
			strNodo.Append("</");
			strNodo.Append(PlantillaXml.Width.ToString());
			strNodo.Append(">");
			
			strNodo.Append("<");
			strNodo.Append(PlantillaXml.Height.ToString());
			strNodo.Append(">");
			strNodo.Append(gEtiqueta.MaxHeight);
			strNodo.Append("</");
			strNodo.Append(PlantillaXml.Height.ToString());
			strNodo.Append(">");
			
			strNodo.Append("<");
			strNodo.Append(PlantillaXml.BackGroundBorder.ToString());
			strNodo.Append(">");
			strNodo.Append(((Color)gEtiqueta.Background.GetValue(SolidColorBrush.ColorProperty)).ToArgb());
			strNodo.Append("</");
			strNodo.Append(PlantillaXml.BackGroundBorder.ToString());
			strNodo.Append(">");
			
			strNodo.Append("<");
			strNodo.Append(PlantillaXml.BackGroundText.ToString());
			strNodo.Append(">");
			strNodo.Append(((Color)txtBody.Background.GetValue(SolidColorBrush.ColorProperty)).ToArgb());
			strNodo.Append("</");
			strNodo.Append(PlantillaXml.BackGroundText.ToString());
			strNodo.Append(">");
			strNodo.Append("</PlantillaEtiqueta>");
			xml.LoadXml(strNodo.ToString());
			return xml.FirstChild;
		}
		public Etiqueta Clone()
		{
			Etiqueta clon=new Etiqueta();
			clon.PonerPlantilla(this);
			clon.Codigo=Codigo;
			clon.NombreArticulo=NombreArticulo;
			return clon;
		}
		public  bool Iguales(object obj)
		{
			Etiqueta other=obj as Etiqueta;
			bool iguales=other!=null;
			if(iguales)
				iguales=other.Codigo==Codigo&&other.NombreArticulo==NombreArticulo&&gEtiqueta.Height.Equals(other.gEtiqueta.Height)&&gEtiqueta.Width.Equals(other.gEtiqueta.Width);
			return iguales;
		}
		public static Etiqueta[] ImportarDesdeXml(string pathXml)
		{
			XmlDocument xml=new XmlDocument();
			xml.Load(pathXml);
			return ImportarDesdeXml(xml);
		}
		public static Etiqueta[] ImportarDesdeXml(XmlDocument xml)
		{
			Etiqueta plantilla;
			Etiqueta[] etiquetas;			
			//cargo la plantilla
			plantilla=new Etiqueta();
			plantilla.PonerPlantilla(xml.FirstChild.FirstChild);
			//cargo las etiquetas
			etiquetas=new Etiqueta[xml.FirstChild.LastChild.ChildNodes.Count];
			for(int i=0;i<etiquetas.Length;i++){
				etiquetas[i]=new Etiqueta(xml.FirstChild.LastChild.ChildNodes[i]);
				etiquetas[i].PonerPlantilla(plantilla);
			}
			return etiquetas;
		}
		
		public static XmlDocument ExportarXml(Etiqueta plantilla,IList<Etiqueta> etiquetas)
		{
			StringBuilder strXml=new StringBuilder();
			XmlDocument xml=new XmlDocument();
			strXml.Append("<EtiquetasExpress>");
			strXml.Append(plantilla.GetPlantillaXmlNode().OuterXml);
			strXml.Append("<Etiquetas>");
			for(int i=0;i<etiquetas.Count;i++)
				strXml.Append(etiquetas[i].ToXmlNode().OuterXml);
			strXml.Append("</Etiquetas>");
			strXml.Append("</EtiquetasExpress>");
			
			xml.LoadXml(strXml.ToString());
			xml.Normalize();
			return xml;
		}
		public static string ExportarCsv(IList<Etiqueta> etiquetas,char separador=';')
		{
			const char ENTER='\n';
			StringBuilder strSeparador=new StringBuilder();
			StringBuilder strEtiquetasCsv=new StringBuilder();
			bool esImpar=etiquetas.Count%2!=0;
			for(int i=0;i<etiquetas[0].gEtiqueta.ColumnDefinitions.Count;i++)
				strSeparador.Append(separador);
			for(int i=0;esImpar?i<etiquetas.Count-1:i<etiquetas.Count;i+=2)
			{
				strEtiquetasCsv.Append(ENTER);
				//fila border;separacionBlanco;fila borde//enter
				strEtiquetasCsv.Append(strSeparador);
				strEtiquetasCsv.Append(separador);//separacion en blanco
				strEtiquetasCsv.Append(strSeparador);
				strEtiquetasCsv.Append(ENTER);
				//borde;codigo1;borde;separacionBlanco;borde;codigo2;borde//enter
				strEtiquetasCsv.Append(separador);//Borde
				strEtiquetasCsv.Append(etiquetas[i].Codigo);
				strEtiquetasCsv.Append(separador);//Borde
				strEtiquetasCsv.Append(separador);//separacion en blanco
				//fila border;separacionBlanco;fila borde//enter
				strEtiquetasCsv.Append(separador);//Borde
				strEtiquetasCsv.Append(etiquetas[i+1].Codigo);
				strEtiquetasCsv.Append(separador);//Borde
				strEtiquetasCsv.Append(separador);//separacion en blanco
				//enter
				strEtiquetasCsv.Append(ENTER);
				//borde;nombre1;borde;separacionBlanco;borde;nombre2;borde//enter
				strEtiquetasCsv.Append(separador);//Borde
				strEtiquetasCsv.Append(etiquetas[i].NombreArticulo);
				strEtiquetasCsv.Append(separador);//Borde
				strEtiquetasCsv.Append(separador);//separacion en blanco
				//fila border;separacionBlanco;fila borde//enter
				strEtiquetasCsv.Append(separador);//Borde
				strEtiquetasCsv.Append(etiquetas[i+1].NombreArticulo);
				strEtiquetasCsv.Append(separador);//Borde
				strEtiquetasCsv.Append(separador);//separacion en blanco
				//enter
				strEtiquetasCsv.Append(ENTER);
				//fila border;separacionBlanco;fila borde//enter
				strEtiquetasCsv.Append(strSeparador);
				strEtiquetasCsv.Append(separador);//separacion en blanco
				strEtiquetasCsv.Append(strSeparador);
				strEtiquetasCsv.Append(ENTER);
				
			}
			if(esImpar)
			{
				//falta poner la ultima fila :D
				//fila border;separacionBlanco;fila borde//enter
				strEtiquetasCsv.Append(strSeparador);
				strEtiquetasCsv.Append(ENTER);
				//borde;codigo1;borde;separacionBlanco;borde;codigo2;borde//enter
				strEtiquetasCsv.Append(separador);//Borde
				strEtiquetasCsv.Append(etiquetas[etiquetas.Count-1].Codigo);
				strEtiquetasCsv.Append(separador);//Borde
				strEtiquetasCsv.Append(separador);//separacion en blanco

				//enter
				strEtiquetasCsv.Append(ENTER);
				//borde;nombre1;borde;separacionBlanco;borde;nombre2;borde//enter
				strEtiquetasCsv.Append(separador);//Borde
				strEtiquetasCsv.Append(etiquetas[etiquetas.Count-1].NombreArticulo);
				strEtiquetasCsv.Append(separador);//Borde
				strEtiquetasCsv.Append(separador);//separacion en blanco
				//enter
				strEtiquetasCsv.Append(ENTER);
				//fila border;separacionBlanco;fila borde//enter
				strEtiquetasCsv.Append(strSeparador);
				strEtiquetasCsv.Append(ENTER);
			}
			return strEtiquetasCsv.ToString();
		}
		
	}
}