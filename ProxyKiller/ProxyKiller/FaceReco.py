import cognitive_face as CF
import requests
from io import BytesIO
from PIL import Image, ImageDraw
import glob
import pymongo

def getRectangle(faceDictionary):
    rect = faceDictionary['faceRectangle']
    left = rect['left']
    top = rect['top']
    bottom = left + rect['height']
    right = top + rect['width']
    return ((left, top), (bottom, right))

KEY = '19cbaf8a3a224dd687d5fe31f6ef353b'  # Replace with a valid subscription key (keeping the quotes in place).
CF.Key.set(KEY)
PERSON_GROUP_ID = 'photos-of-me-part2'
BASE_URL = 'https://centralindia.api.cognitive.microsoft.com/face/v1.0'  # Replace with your regional Base URL
CF.BaseUrl.set(BASE_URL)

CF.person_group.create(PERSON_GROUP_ID,'Photos of me part 2')

Name = "Kamal Sahoo"
response = CF.person.create(PERSON_GROUP_ID,Name)
Id = response['personId']

PId1=CF.person.add_face('src1.jpg',PERSON_GROUP_ID,Id)
PId2=CF.person.add_face('src2.jpg',PERSON_GROUP_ID,Id)
PId3=CF.person.add_face('src3.jpg',PERSON_GROUP_ID,Id)
PId4=CF.person.add_face('src4.jpg',PERSON_GROUP_ID,Id)
PId5=CF.person.add_face('src5.jpg',PERSON_GROUP_ID,Id)

for person in CF.person.lists(PERSON_GROUP_ID):
    print(person)
    print()

print()

CF.person_group.train(PERSON_GROUP_ID)
response = CF.person_group.get_status(PERSON_GROUP_ID)
status = response['status']
print(status)

print()

faces = CF.face.detect('test.jpg')
img = Image.open('test.jpg')

#For each face returned use the face rectangle and draw a red box.
draw = ImageDraw.Draw(img)
for face in faces:
    draw.rectangle(getRectangle(face), outline='red')
img.save('img.jpg')
#Display the image in the users default image browser.
img.show()


face_ids = [d['faceId'] for d in faces]

for face in face_ids:
    print(face)
    print()
print()

identified_faces = CF.face.identify(face_ids, PERSON_GROUP_ID)

for face in identified_faces:
    print(face)
    print()
print()