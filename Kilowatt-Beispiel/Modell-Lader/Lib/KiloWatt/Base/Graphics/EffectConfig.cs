using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KiloWatt.Base.Graphics
{
  public class EffectConfig
  {
    public const int NumBones = 58;

    Effect fx_;
    EffectParameter worldViewProj_;
    EffectParameter world_;
    EffectParameter worldView_;
    EffectParameter view_;
    EffectParameter projection_;
    EffectParameter viewInv_;
    EffectParameter viewProj_;
    EffectParameter lightDir_;
    EffectParameter lightDiffuse_;
    EffectParameter lightAmbient_;
    EffectParameter fogDistance_;
    EffectParameter fogColor_;
    EffectParameter fogHeight_;
    EffectParameter fogDepth_;
    EffectParameter frame_;
    EffectParameter pose_;
    string name_;
    public Vector4[] PoseData;
    public bool HasPose { get { return pose_ != null; } }

    public Effect FX { get { return fx_; } set { fx_ = value; Reconfig(); } }
    public string Name { get { return name_; } }
    
    public EffectConfig(Effect fx, string name)
    {
      fx_ = fx;
      name_ = name;
      Reconfig();
    }

    public void Setup(DrawDetails dd)
    {
      Setup(dd, dd.world);
    }

    public void Setup(DrawDetails dd, Matrix worldOverride)
    {
      if (lightAmbient_ != null) lightAmbient_.SetValue(dd.lightAmbient);
      if (lightDiffuse_ != null) lightDiffuse_.SetValue(dd.lightDiffuse);
      if (lightDir_ != null) lightDir_.SetValue(dd.lightDir);
      if (viewInv_ != null) viewInv_.SetValue(dd.viewInv);
      if (viewProj_ != null) viewProj_.SetValue(dd.viewProj);
      if (world_ != null) world_.SetValue(worldOverride);
      if (worldView_ != null) worldView_.SetValue(worldOverride * dd.view);
      if (view_ != null) view_.SetValue(dd.view);
      if (projection_ != null) projection_.SetValue(dd.projection);
      if (worldViewProj_ != null) worldViewProj_.SetValue(worldOverride * dd.viewProj);
      if (fogDistance_ != null) fogDistance_.SetValue(dd.fogDistance);
      if (fogColor_ != null)
        if (fogColor_.ColumnCount == 3)
          fogColor_.SetValue(new Vector3(dd.fogColor.X, dd.fogColor.Y, dd.fogColor.Z));
        else
          fogColor_.SetValue(dd.fogColor);
      if (fogHeight_ != null) fogHeight_.SetValue(dd.fogHeight);
      if (fogDepth_ != null) fogDepth_.SetValue(dd.fogDepth);
      if (frame_ != null)
        frame_.SetValue(dd.frame);
      if (pose_ != null)
        pose_.SetValue(PoseData);
    }
    
    void Reconfig()
    {
      worldViewProj_ = fx_.Parameters["WorldViewProjection"];
      world_ = fx_.Parameters["World"];
      worldView_ = fx_.Parameters["WorldView"];
      view_ = fx_.Parameters["View"];
      projection_ = fx_.Parameters["Projection"];
      viewInv_ = fx_.Parameters["ViewInverse"];
      viewProj_ = fx_.Parameters["ViewProjection"];
      lightDir_ = fx_.Parameters["LightDirection"];
      lightDiffuse_ = fx_.Parameters["LightColor"];
      lightAmbient_ = fx_.Parameters["LightAmbient"];
      fogDistance_ = fx_.Parameters["FogDistance"];
      fogColor_ = fx_.Parameters["FogColor"];
      fogHeight_ = fx_.Parameters["FogHeight"];
      fogDepth_ = fx_.Parameters["FogDepth"];
      frame_ = fx_.Parameters["Frame"];
      pose_ = fx_.Parameters["Pose"];
    }
  }

  public interface IEffectProvider
  {
    Effect[] Effects { get; }
    string[] EffectNames { get; }
  }
}
