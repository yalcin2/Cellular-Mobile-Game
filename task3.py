import cv2
import numpy as np

camera = cv2.VideoCapture(0)
face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + "haarcascade_frontalface_default.xml")
loop = True

while loop:
    _, image = camera.read()
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

    counter = 0

    faces = face_cascade.detectMultiScale(gray, 1.3, 5)

    for (x, y, w, h) in faces:
        cv2.rectangle(image, (x,y), (x+w, y+h), (0, 255, 0), 2)
        cv2.rectangle(image, (x-40, y-90), (x+w + 40, y+h+90), (0, 255, 255), 2)
        roi = image[y-90:y+h+90, x-40:x+w+40]
        counter = counter + 1;

    cv2.imshow("Frame", image)
    key = cv2.waitKey(1)

    if key == 32:
        cv2.imwrite("savedFrame%d.png" % counter, roi)

camera.release()
cv2.destroyAllWindows()
