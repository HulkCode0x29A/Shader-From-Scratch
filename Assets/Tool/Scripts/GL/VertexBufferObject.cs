using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class VertexBufferPointer
{
    //vertices attribute index
    public int index;

    //number of components per vertex attribtue  muse be 1,2,3,4
    public int size;

    //offset between consecutive vertex attribtues
    public int stride;

    public int offset;
}
public class VertexBufferObject
{
    public int ID { get; set; }

    public List<float> buffers = new List<float>();
    //record index we will loop index data from buffers
    private int buffersIndex = 0;
    //this list define hao we map data to shader
    private List<VertexBufferPointer> pointerList = new List<VertexBufferPointer>();

    private ShaderBase cacheShader;
    //cache current shader fields
    private Dictionary<int, FieldInfo> shaderFieldsDict = new Dictionary<int, FieldInfo>();


    public void SetBuffers(List<float> buffers)
    {
        this.buffers = buffers;
    }

    public void Reset()
    {
        this.buffersIndex = 0;
    }

    public void VertexAttribPointer(int index, int size, int stride, int offset)
    {
        VertexBufferPointer p = new VertexBufferPointer();
        p.index = index;
        p.size = size;
        p.stride = stride;
        p.offset = offset;

        pointerList.Add(p);
    }

    /// <summary>
    /// set up shader fields
    /// </summary>
    /// <param name="shader"></param>
    public void SetUpShader(ShaderBase shader)
    {
        this.cacheShader = shader;
        shaderFieldsDict.Clear();

        FieldInfo[] fields = shader.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo info = fields[i];

            Attribute attribute = Attribute.GetCustomAttribute(info, typeof(VertexLayoutAttribute));
            if (null != attribute)
            {
                VertexLayoutAttribute layoutAttribute = (VertexLayoutAttribute)attribute;

                int layoutIndex = layoutAttribute.Index;
                shaderFieldsDict.Add(layoutIndex, info);
            }
        }
    }

    public void AnalysisVertexBuffer()
    {
        int localPointer = 0;
        for (int i = 0; i < pointerList.Count; i++)
        {
            VertexBufferPointer p = pointerList[i];
            int index = p.index;
            int size = p.size;
            int stride = p.stride;
            int offset = p.offset;

            localPointer = buffersIndex;

            Type type = typeof(float);
            object value;
            if (size == 1)
            {
                value = buffers[localPointer];
                localPointer += 1;
            }
            if (size == 2)
            {
                type = typeof(Vector2);
                value = new Vector2(buffers[localPointer], buffers[localPointer + 1]);
                localPointer += 2;
            }
            else if (size == 3)
            {
                type = typeof(Vector3);
                value = new Vector3(buffers[localPointer], buffers[localPointer + 1], buffers[localPointer + 2]);
                localPointer += 3;
            }
            else
            {
                type = typeof(Vector4);
                value = new Vector4(buffers[localPointer], buffers[localPointer + 1], buffers[localPointer + 2], buffers[localPointer + 3]);
                localPointer += 4;
            }

            if (shaderFieldsDict.ContainsKey(index))
            {
                FieldInfo info = shaderFieldsDict[index];
                if (info.FieldType == type)
                    info.SetValue(cacheShader, value);
                else
                    Debug.LogError("error type try set :" + type + " to:" + info.FieldType + "  name:" + info.Name + "  size:" + size);

            }

            buffersIndex += size;
        }
    }


}
