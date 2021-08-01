using UnityEngine;
using TMPro;

// 글자 꿈틀거리는 효과 
public class TitleEffect : MonoBehaviour
{
    public TMP_Text tmp;

    private void Update()
    {
        tmp.ForceMeshUpdate();
        TMP_TextInfo textInfo = tmp.textInfo;

        // 글자수 만큼 반복  
        for(int i = 0; i < textInfo.characterCount; i++)
        {
            // 글자정보 저장 
            var charInfo = textInfo.characterInfo[i];

            // 글자가 visible 하지 않으면 continue 
            if (!charInfo.isVisible) continue;

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            // 글자의 4개의 정점들을 탐색 
            for(int j = 0; j < 4; j++)
            {
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 10f, 0);
            }            
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;

            tmp.UpdateGeometry(meshInfo.mesh, i);
        }   
    }
}
