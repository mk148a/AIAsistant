import tensorflow as tf
import numpy as np
import os
import json

def load_data(data_dir):
    with open(os.path.join(data_dir, 'labels.json')) as f:
        veri = json.load(f)

   
    data = []
    labels = []

    for label, dosya_adi in veri.items():
        filepath = os.path.join(data_dir, dosya_adi)
        audio_binary = tf.io.read_file(filepath)
        audio, _ = tf.audio.decode_wav(audio_binary)
        # Ses dosyasını sabit bir uzunluğa getirme (örneğin, 16000 örneğe)
        audio = tf.image.resize(audio, (16000, 1))
        data.append(audio.numpy().flatten())  # Diziyi düzleştirme
        labels.append(label)
    
    return np.array(data), np.array(labels)

data_dir = 'SoundRecords'
data, labels = load_data(data_dir)

model = tf.keras.Sequential([
    tf.keras.layers.Input(shape=(16000,)),  # Düzleştirilmiş ses dosyaları için giriş şekli
    tf.keras.layers.Reshape((16000, 1)),  # CNN katmanları için giriş şeklini yeniden ayarla
    tf.keras.layers.Conv1D(16, 3, activation='relu'),
    tf.keras.layers.MaxPooling1D(),
    tf.keras.layers.Flatten(),
    tf.keras.layers.Dense(64, activation='relu'),
    tf.keras.layers.Dense(len(set(labels)), activation='softmax')
])

model.compile(optimizer='adam', loss='sparse_categorical_crossentropy', metrics=['accuracy'])

model.fit(data, labels, epochs=10)

model.save('model.h5')