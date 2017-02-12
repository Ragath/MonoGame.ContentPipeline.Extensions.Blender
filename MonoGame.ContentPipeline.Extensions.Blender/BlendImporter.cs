using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace MonoGame.ContentPipeline.Extensions.Blender
{
    [ContentImporter(".blend", DisplayName = "Blender Importer", DefaultProcessor = "ModelProcessor")]
    public class BlendImporter : FbxImporter
    {

        public override NodeContent Import(string filename, ContentImporterContext context)
        {
            var input = Path.GetFullPath(filename);
            if (!File.Exists(input))
                throw new FileNotFoundException();


            var tmp = Path.GetTempFileName();
            var fbxPath = Path.ChangeExtension(tmp, "fbx");
            File.Delete(tmp);

            try
            {
                BlenderAdapter.ToFbx(input, fbxPath);
                return base.Import(fbxPath, context);
            }
            finally
            {
                File.Delete(fbxPath);
            }
        }
    }

}
