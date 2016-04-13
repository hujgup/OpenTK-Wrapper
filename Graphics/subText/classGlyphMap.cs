using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

namespace Graphics {
	public class GlyphMap : IAttributeChangeListener, IDictionary<char,Bitmap>, ICollection<KeyValuePair<char,Bitmap>>, IEnumerable<KeyValuePair<char,Bitmap>>, IEnumerable {
		private IDictionary<char,Bitmap> _map;
		public GlyphMap(IDictionary<char,Bitmap> dictionary) {
			_map = new Dictionary<char,Bitmap>(dictionary);
		}
		public GlyphMap(IEnumerable<KeyValuePair<char,Bitmap>> collection) : this() {
			IEnumerator<KeyValuePair<char,Bitmap>> enumerator = collection.GetEnumerator();
			while (enumerator.MoveNext()) {
				_map.Add(enumerator.Current.Key,enumerator.Current.Value);
			}
		}
		public GlyphMap(params KeyValuePair<char,Bitmap>[] map) : this((IEnumerable<KeyValuePair<char,Bitmap>>)map) {
		}
		public GlyphMap(int capacity) {
			_map = new Dictionary<char,Bitmap>(capacity);
		}
		public GlyphMap() {
			_map = new Dictionary<char,Bitmap>();
		}
		public Bitmap this[char key] {
			get {
				return _map[key];
			}
			set {
				_map[key] = value;
				OnAttributeChange();
			}
		}
		public int Count {
			get {
				return _map.Count;
			}
		}
		public bool IsReadOnly {
			get {
				return false;
			}
		}
		public ICollection<char> Keys {
			get {
				return _map.Keys;
			}
		}
		public ICollection<Bitmap> Values {
			get {
				return _map.Values;
			}
		}
		private void OnAttributeChange() {
			if (AttributeChange != null) {
				AttributeChange(this,EventArgs.Empty);
			}
		}
		/// <summary>
		/// Occurs when an attribute's value changes.
		/// </summary>
		public event EventHandler AttributeChange;
		private IDictionary CnvIDic() {
			return (IDictionary)_map;
		}
		public void Add(char key,Bitmap value) {
			_map.Add(key,value);
			OnAttributeChange();
		}
		public bool Contains(object key) {
			return CnvIDic().Contains(key);
		}
		public bool ContainsKey(char key) {
			return _map.ContainsKey(key);
		}
		public void Clear() {
			_map.Clear();
			OnAttributeChange();
		}
		public bool Remove(char key) {
			bool res = _map.Remove(key);
			OnAttributeChange();
			return res;
		}
		public bool TryGetValue(char key,out Bitmap value) {
			return _map.TryGetValue(key,out value);
		}
		public IEnumerator<KeyValuePair<char,Bitmap>> GetEnumerator() {
			return _map.GetEnumerator();
		}
		void ICollection<KeyValuePair<char,Bitmap>>.Add(KeyValuePair<char,Bitmap> kvp) {
			Add(kvp.Key,kvp.Value);
			OnAttributeChange();
		}
		void ICollection<KeyValuePair<char,Bitmap>>.CopyTo(KeyValuePair<char,Bitmap>[] array,int arrayIndex) {
			_map.CopyTo(array,arrayIndex);
		}
		bool ICollection<KeyValuePair<char,Bitmap>>.Contains(KeyValuePair<char,Bitmap> kvp) {
			return _map.Contains(kvp);
		}
		bool ICollection<KeyValuePair<char,Bitmap>>.Remove(KeyValuePair<char,Bitmap> kvp) {
			bool res = _map.Remove(kvp);
			OnAttributeChange();
			return res;
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
	}
}

