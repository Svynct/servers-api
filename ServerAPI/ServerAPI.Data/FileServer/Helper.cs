using System;
using System.IO;

namespace ServerAPI.Data.FileServer
{
    public static class Helper
    {
        public static bool SalvarVideo(Guid serverId, string fileName, string base64video)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + $".Data\\FileServer\\Videos\\{serverId}";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path += "\\" + fileName;

                File.WriteAllText(path, base64video);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool RemoverVideo(Guid serverId, string fileName)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + $".Data\\FileServer\\Videos\\{serverId}";

                if (!Directory.Exists(path))
                    throw new Exception("O servidor inserido não possui vídeos vinculados a ele.");

                path += "\\" + fileName;

                if (!File.Exists(path))
                    throw new Exception("O vídeo inserido não existe.");

                File.Delete(path);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RecuperarConteudoDoVideo(Guid serverId, string fileName)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + $".Data\\FileServer\\Videos\\{serverId}";

                if (!Directory.Exists(path))
                    throw new Exception("O servidor inserido não possui vídeos vinculados a ele.");

                path += "\\" + fileName;

                if (!File.Exists(path))
                    throw new Exception("O vídeo inserido não existe.");

                var retorno = File.ReadAllText(path);

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
