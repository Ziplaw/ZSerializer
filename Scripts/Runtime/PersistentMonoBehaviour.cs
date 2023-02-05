using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZSerializer
{
	public sealed class NonZSerialized : Attribute { }

	public sealed class ForceZSerialized : Attribute { }

	public abstract class PersistentMonoBehaviour : MonoBehaviour, IZSerializable
	{
		/// <summary>
		/// OnPreSave is called right before any Save occurs
		/// </summary>
		public virtual void OnPreSave( ) { }

		/// <summary>
		/// OnPostSave is called right after any Save occurs
		/// </summary>
		public virtual void OnPostSave( ) { }

		/// <summary>
		/// OnPreLoad is called right before any Load occurs
		/// </summary>
		public virtual void OnPreLoad( ) { }

		/// <summary>
		/// OnPostLoad is called right after any Load occurs
		/// </summary>
		public virtual void OnPostLoad( ) { }

		public List<string> GetZUIDList( ) => new List<string> { ZUID, GOZUID };

		[NonZSerialized, HideInInspector] public bool showSettings;

		[NonZSerialized, HideInInspector, SerializeField]
		internal bool isOn = true;

		[ForceZSerialized, HideInInspector, SerializeField]
		internal int groupID;

		[ForceZSerialized, HideInInspector, SerializeField]
		internal bool autoSync = true;

		[NonZSerialized, SerializeField, HideInInspector]
		private string _zuid;

		[NonZSerialized, SerializeField, HideInInspector]
		private string _gozuid;

		public int GroupID
		{
			get => groupID;
			set
			{
				if ( AutoSync )
				{
					ZSerializerSettings.Instance.componentDataDictionary[GetType( )].groupID = value;

					foreach ( var o in FindObjectsOfType( GetType( ) ) )
					{
						if ( ( (PersistentMonoBehaviour)o ).AutoSync )
						{
							( (PersistentMonoBehaviour)o ).groupID = value;
							#if UNITY_EDITOR
							EditorUtility.SetDirty( o );
							#endif
						}
					}
				}
				else
					groupID = value;
			}
		}

		public bool AutoSync
		{
			get => autoSync;
			set => autoSync = value;
		}

		public string ZUID
		{
			get => _zuid;
			set => _zuid = value;
		}

		public string GOZUID
		{
			get => _gozuid;
			set => _gozuid = value;
		}

		public bool IsOn
		{
			get => isOn;
			set => isOn = value;
		}

		public bool IsSaving { get; set; }

		public bool IsLoading { get; set; }

		protected virtual void Awake( )
		{
			GenerateZUIDs( false, true, false );
			AddZUIDsToIDMap(  );
		}

		public virtual void Reset( )
		{
			IsOn = ZSerializerSettings.Instance.componentDataDictionary[GetType( )].isOn;
			GroupID = ZSerializerSettings.Instance.componentDataDictionary[GetType( )].groupID;
			GenerateZUIDs( false, true, false );
		}

		public void GenerateZUIDs( bool overrideIDs, bool getGOZUIDFromAttachedZSerializables, bool generateZUIDsForNeighbors )
		{
			if ( overrideIDs || string.IsNullOrEmpty( ZUID ) ) ZUID = Guid.NewGuid( ).ToString( );

			if ( overrideIDs || string.IsNullOrEmpty( GOZUID ) )
			{
				if ( getGOZUIDFromAttachedZSerializables )
				{
					var zs = GetComponents<IZSerializable>( ).FirstOrDefault( z => !string.IsNullOrEmpty( z.GOZUID ) && z.GOZUID != GOZUID );

					if ( zs != null ) GOZUID = zs.GOZUID;
					else GOZUID = ZSerialize.GetZUID( );
				}
				else GOZUID = ZSerialize.GetZUID( );
			}

			#if UNITY_EDITOR
			EditorUtility.SetDirty( this );
			PrefabUtility.RecordPrefabInstancePropertyModifications( this );
			#endif

			if ( generateZUIDsForNeighbors )
				foreach ( var monoBehaviour in GetComponents<IZSerializable>( ).Where( c => !ReferenceEquals( c, this ) ) )
				{
					monoBehaviour.GenerateZUIDs( overrideIDs, true, false );
				}
		}

		public void AddZUIDsToIDMap( )
		{
			if ( ZUID == null ) return;

			ZSerialize.idMap[GroupID].TryAddToDictionary( ZUID, this );
			ZSerialize.idMap[GroupID].TryAddToDictionary( GOZUID, gameObject );
		}
	}
}