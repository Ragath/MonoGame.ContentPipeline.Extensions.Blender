using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace MonoGame.ContentPipeline.Extensions.Blender
{
    public static class BlenderAdapter
    {
        static string[] StandardBlenderPaths { get; } = { "blender.exe", @"C:\Program Files\Blender Foundation\Blender\blender.exe" };
        static string BlenderEXE { get; set; }
        public static string PythonScript { get; } =
            "import bpy;" +
            "import sys;" +
            //"for ob in bpy.data.objects: if ob.type=='ARMATURE': ob.action=bpy.data.actions[-1];" +
            "bpy.ops.export_scene.fbx(filepath=sys.argv[sys.argv.index('--') + 1], " +
            "apply_unit_scale=False, object_types={'ARMATURE', 'MESH'}, " +
            "axis_forward='-Z', " +
            "axis_up='Y', " +
            "bake_anim_use_nla_strips=False, " +
            "add_leaf_bones=False, " +
            "use_anim_action_all=True);";

        /// <summary>
        /// Export fbx with Forward: -Z, Up: Y
        /// </summary>
        /// <exception cref="FileNotFoundException">Could not locate blender.exe</exception>
        /// <exception cref="Exception">blender export operation failed.</exception>
        /// <param name="input">*.blend input</param>
        /// <param name="output">*.fbx output</param>
        public static void ToFbx(string input, string output)
        {
            if (BlenderEXE != null)
                ToFbx(BlenderEXE, input, output);
            else
            {
                foreach (var path in StandardBlenderPaths)
                    try
                    {
                        ToFbx(path, input, output);
                        BlenderEXE = path;
                        return;
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (Win32Exception)
                    {
                    }
                throw new FileNotFoundException("Could not find blender.exe");
            }
        }

        public static void ToFbx(string blenderPath, string input, string output)
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = Path.GetFileName(blenderPath),
                WindowStyle = ProcessWindowStyle.Hidden,
                WorkingDirectory = Path.GetDirectoryName(blenderPath),
                Arguments = $"\"{Path.GetFullPath(input)}\" --background --python-expr \"{PythonScript}\" -- \"{output}\""
            };

            Process.Start(startInfo).WaitForExit();
            if (!File.Exists(output))
                throw new Exception("Blender fbx export failed");
        }
    }
}
