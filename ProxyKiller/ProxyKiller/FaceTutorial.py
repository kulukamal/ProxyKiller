import cognitive_face as CF
import requests
from io import BytesIO
from PIL import Image, ImageDraw

def getRectangle(faceDictionary):
    rect = faceDictionary['faceRectangle']
    left = rect['left']
    top = rect['top']
    bottom = left + rect['height']
    right = top + rect['width']
    return ((left, top), (bottom, right))

img_url = 'http://www.allwhitebackground.com/images/3/3813.jpg'
KEY = '19cbaf8a3a224dd687d5fe31f6ef353b'  # Replace with a valid subscription key (keeping the quotes in place).
CF.Key.set(KEY)

BASE_URL = 'https://centralindia.api.cognitive.microsoft.com/face/v1.0'  # Replace with your regional Base URL
CF.BaseUrl.set(BASE_URL)

# You can use this example JPG or replace the URL below with your own URL to a JPEG image.

faces = CF.face.detect(img_url)
#Download the image from the url
response = requests.get(img_url)
img = Image.open(BytesIO(response.content))

#For each face returned use the face rectangle and draw a red box.
draw = ImageDraw.Draw(img)
for face in faces:
    draw.rectangle(getRectangle(face), outline='red')
img.save('img.jpg')
print('hello world')
#Display the image in the users default image browser.
img.show()




