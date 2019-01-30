import pymongo as pm
import cognitive_face as CF
import requests
from io import BytesIO
from PIL import Image, ImageDraw
import glob
import tkinter as tk
from tkinter import filedialog

KEY = '19cbaf8a3a224dd687d5fe31f6ef353b'  # Replace with a valid subscription key (keeping the quotes in place).
CF.Key.set(KEY)
PERSON_GROUP_ID = 'photos'
BASE_URL = 'https://centralindia.api.cognitive.microsoft.com/face/v1.0'  # Replace with your regional Base URL
CF.BaseUrl.set(BASE_URL)

client = pm.MongoClient()
db = client["ProxyKiller"]
buffer = db["buffer"]
'''CF.person_group.delete(PERSON_GROUP_ID)
CF.person_group.create(PERSON_GROUP_ID,'My group')

Response = CF.person.create(PERSON_GROUP_ID,'kamal sahoo')
Id = Response['personId']
myDict = {
    "_id" : Id
}

buffer.insert_one(myDict)
print(Id)



PId1=CF.person.add_face('src1.jpg',PERSON_GROUP_ID,Id)
PId2=CF.person.add_face('src2.jpg',PERSON_GROUP_ID,Id)
PId3=CF.person.add_face('src3.jpg',PERSON_GROUP_ID,Id)
PId4=CF.person.add_face('src4.jpg',PERSON_GROUP_ID,Id)
PId5=CF.person.add_face('src5.jpg',PERSON_GROUP_ID,Id)

CF.person_group.train(PERSON_GROUP_ID)

response = CF.person_group.get_status(PERSON_GROUP_ID)
status = response['status']
print(status)
print()'''

Id = buffer['_id']

faces = CF.face.detect('test.jpg')
face_ids = [d['faceId'] for d in faces]

identified_faces = CF.face.identify(face_ids, PERSON_GROUP_ID)

for face in identified_faces:
    if(len(face['candidates'])!=0):
        print(face['candidates'][0])
print()