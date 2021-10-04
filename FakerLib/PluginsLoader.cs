using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FakerLib.Generators;

namespace FakerLib
{
    public class PluginsLoader
    {
        private readonly string _pluginsPath;

        public PluginsLoader(string pluginsPath)
        {
            _pluginsPath = pluginsPath;
        }
        
        public Dictionary<Type, ITypeGenerator> ImportAll()
        {
            var pluginDirectory = new DirectoryInfo(_pluginsPath);
            if (!pluginDirectory.Exists)
                pluginDirectory.Create();
    
            var pluginFiles = Directory.GetFiles(_pluginsPath, "*.dll");
            
            Dictionary<Type, ITypeGenerator> plugins = new ();
            
            foreach (var file in pluginFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(file);
                    
                    foreach(var type in assembly.GetExportedTypes())
                    {
                        if (type.IsAssignableTo(typeof(ITypeGenerator<>)) ||
                            type.IsAssignableTo(typeof(ITypeGenerator))) 
                        {
                            if (Activator.CreateInstance(type) is ITypeGenerator generator)
                            {
                                if (generator.Generate().GetType() == generator.GetGeneratingType())
                                {
                                    plugins.Add(generator.GetGeneratingType(), generator);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return plugins;
        }
    }
}