using UnityEngine;
using UnityEditor;


class MyTexturePostprocessor : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        if (assetPath.Contains("T_") || assetPath.Contains("Sprite_"))
        {

            TextureImporter textureImporter = (TextureImporter)assetImporter;
            //textureImporter.isReadable = false;   //메모리프로파일러 문제 발생
            textureImporter.mipmapEnabled = false;
            textureImporter.maxTextureSize = 512;

            TextureImporterPlatformSettings tips = new TextureImporterPlatformSettings();
            tips.overridden = true;
            tips.maxTextureSize = 512;
            tips.name = "Android";
            tips.compressionQuality = 50;
            tips.format = TextureImporterFormat.ASTC_4x4;

            textureImporter.SetPlatformTextureSettings(tips);
        }
    }
}