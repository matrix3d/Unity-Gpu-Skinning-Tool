using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Framework.GpuSkinning;


public class GpuSkinningInstance : MonoBehaviour {

    public GpuSkinningAnimData textAsset;
    
    [SerializeField]
    public string defaultClipName = null;

    private Material _material;
	public Material AnimMaterial
	{
		get { return _material; }
		set { 
				if (_material != value)
				{
					_material = value; 
					refreshInstance();
				}
			}
	}

	private GpuSkinningAnimData anim_data;
    private MaterialPropertyBlock _mpb;
    private MeshRenderer _meshRenderer;

	void Awake() 
	{
		_meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.sharedMaterial;
        _mpb = new MaterialPropertyBlock();
	}

	// Use this for initialization
	void Start () 
	{
		refreshInstance();
        if (!string.IsNullOrEmpty(defaultClipName))
        {
            Play(defaultClipName);
        }
        else if (anim_data != null && anim_data.clips.Length > 0)
        {
             Play(anim_data.clips[0].name);
        }
	}

	void refreshInstance()
    {
        if (textAsset == null)
        {
            return;
        }

        anim_data = textAsset;

        if (_material != null)
        {
            _material.SetInt("_BoneNum", anim_data.totalBoneNum);
        }
	}

    private GpuSkinningAnimClip _playClip;
    private float _startTime;
    private void Play(string clipName)
    {
        if (anim_data == null)
            return;
        
        for (int i=0; i<anim_data.clips.Length; ++i)
        {
            if (anim_data.clips[i].name == clipName)
            {
                _playClip = anim_data.clips[i];
                _startTime = Time.time;
                return;
            }
        }
    }

    // Update is called once per frame
	void Update ()
	{
        if (_material != null && _playClip != null)
        {
            float time = Time.time - _startTime;
            int frame = (int)(time * _playClip.frameRate);
            
            // loop
            frame = frame % _playClip.Length();

            int finalFrameIndex = _playClip.startFrame + frame;

            _meshRenderer.GetPropertyBlock(_mpb);
            _mpb.SetInt("_FrameIndex", finalFrameIndex);
            _meshRenderer.SetPropertyBlock(_mpb);
        }
    }
}