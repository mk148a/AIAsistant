import tensorflow as tf
from tensorflow.keras import layers

# MNIST veri kümesini yükleyin
mnist = tf.keras.datasets.mnist
(x_train, y_train), (x_test, y_test) = mnist.load_data()

# Veriyi ön işleyin
x_train, x_test = x_train / 255.0, x_test / 255.0

# Modeli oluşturun
model = tf.keras.Sequential([
    tf.keras.layers.Flatten(input_shape=(28, 28)),
    tf.keras.layers.Dense(128, activation='relu'),
    tf.keras.layers.Dropout(0.2),
    tf.keras.layers.Dense(10, activation='softmax')
])

# Modeli derleyin
model.compile(optimizer='adam',
              loss='sparse_categorical_crossentropy',
              metrics=['accuracy'])

# Modeli eğitin
model.fit(x_train, y_train, epochs=5)

# Modeli değerlendirin
model.evaluate(x_test, y_test)

# Modeli SavedModel formatında kaydedin
model.export("saved_model/my_model")
