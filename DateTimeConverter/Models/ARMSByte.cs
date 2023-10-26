using System;

namespace DateTimeConverter.Models
{
    class ARMSByte : NotifyPropertyChanged
    {
        private byte[] _aRMSPackage = new byte[8];
        private float _value = 0.0f;
        private byte _crc16l = new();
        private byte _crc16h = new();

        private string _valueBytesStr;

        public string ValueBytesStr
        {
            get { return _valueBytesStr; }
            set { _valueBytesStr = value; OnPropertyChanged(); }
        }

        public byte[] ARMSPackage
        {
            get => _aRMSPackage;
            set
            {
                _aRMSPackage = value;
                OnPropertyChanged();
            }
        }
        public Single Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        public byte CRC16L
        {
            get => _crc16l;
            set
            {
                _crc16l = value;
                OnPropertyChanged();
            }
        }

        public byte CRC16H
        {
            get { return _crc16h; }
            set
            {
                _crc16h = value;
                OnPropertyChanged();
            }
        }
    }
}
