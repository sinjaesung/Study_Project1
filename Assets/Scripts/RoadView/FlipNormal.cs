using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FlipNormal : MonoBehaviour
{
    //�븻�� ������ ������� ���ʿ��� �ؽ�ó�� ���̰� �Ѵ�.
    //����Ƽ�� ������ Ư¡(���������� �׸���)�� �̿��Ѵ�.
    //���������� �������� �׸��� �� �� ������ �޽��� ���ؽ�(��)�� ��ġ�� �ٲ㼭 �ݴ�� �׸��� ȿ���� �ִ� Ʈ��

    [SerializeField] private MeshFilter meshfilter;
    // Start is called before the first frame update
    void Start()
    {
        //�޽����� ������Ʈ�� mesh�� �Ҵ��Ѵ�.
        Mesh mesh = meshfilter.mesh;
        if(mesh != null)
        {
            Vector3[] normals = mesh.normals;
            for(int i=0; i<normals.Length; i++)
            {
                Debug.Log($"normals{i}=>{normals[i]}");
                normals[i] *= -1;
            }

            mesh.normals = normals;

            for(int i=0; i<mesh.subMeshCount; i++)
            {
                //�Ž��� Ʈ���̾ޱ��� �����´�.(3���� ��)
                int[] test = mesh.GetTriangles(i);
                for(int t=0; t<test.Length; t+=3)
                {
                    //���� ������ �ٲ۴�. A���� B���� �ٲٴ� ���? A=5,B=3 
                    int temp = test[t];
                    //n���� n+1������,n+1���� n������ ���� �ٲ۴�.
                    test[t] = test[t + 1];
                    test[t + 1] = temp;
                }

                mesh.SetTriangles(test, i);
            }
        }
    }
}

