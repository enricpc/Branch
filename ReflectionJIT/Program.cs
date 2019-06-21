using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ReflectionJIT
{
    class Program
    {
        static void Main(string[] args)
        {
            XPathDocument doc_xml = new XPathDocument("ReflectionConfiguration.xml");

            //Inicializamos el navegador XPath, para hacer consultas
            XPathNavigator navegador = doc_xml.CreateNavigator();
            //Realizamos la consulta con el nodo, para obtener el texto del nodo con id="Alumno"
            XPathNavigator texto_nodes =
                navegador.SelectSingleNode("/Types/Type[@Id='Alumno']");

            Assembly myAssembly = typeof(Program).Assembly;
            //get type of class Alumno from just loaded assembly
            Type alumnoType = myAssembly.GetType(texto_nodes.ToString());

            //create instance of class Alumno
            object objetoDeAlumno =
                Activator.CreateInstance
                (alumnoType, 1, "Pepe", "Soto", "3243434x");

            Console.WriteLine(((Alumno)objetoDeAlumno).Nombre);

            XElement root = XElement.Load("ReflectionConfiguration.xml");
            //Linq to xml
            IEnumerable<XElement> tests =
                from element in root.Elements("Type")
                where (string)element.Attribute("Id") == "Alumno"
                select element;

            string cadena = tests.First().Value;

            //get type of class Calculator from just loaded assembly
            Type alumno1Type = myAssembly.GetType(cadena);

            //Create instance of class Calculator
            object objetoDeAlumno1 =
                Activator.CreateInstance
                (alumno1Type, 1, "Pepe", "Soto", "3243434x");

            Console.WriteLine(((Alumno)objetoDeAlumno1).Nombre);
        }
    }
}
