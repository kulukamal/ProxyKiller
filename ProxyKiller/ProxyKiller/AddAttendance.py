import cognitive_face as CF
import requests
from io import BytesIO
from PIL import Image, ImageDraw
import glob
import pymongo
def Min(a,b):
    if(a<b):
        return a;
    else
        return b;

KEY = '19cbaf8a3a224dd687d5fe31f6ef353b'  # Replace with a valid subscription key (keeping the quotes in place).
CF.Key.set(KEY)
PERSON_GROUP_ID = 'proxykiller'
BASE_URL = 'https://centralindia.api.cognitive.microsoft.com/face/v1.0'  # Replace with your regional Base URL
CF.BaseUrl.set(BASE_URL)

CF.person_group.delete(PERSON_GROUP_ID)
CF.person_group.create(PERSON_GROUP_ID)

client = pymongo.MongoClient()
db = client['ProxyKiller']
buffer = db['buffer']
studentMap = db['studentMap']

subjectId = buffer.find_one({'_id':{'$ne' : '-1'}})['_id']
subject = db[subjectId]

buffer.delete_one({'_id':subjectId})

faces = CF.face.detect('testPicture\test.jpg')
face_ids = [d['faceId'] for d in faces]
i = 0
listUser = []
end = len(face_ids)-1
while True :
    identified_faces = CF.face.identify(face_ids[i:Min(i+9,end)], PERSON_GROUP_ID)
    i = i + 10
    for face in identified_faces:
    if(len(face['candidates'])!=0):
        Id = face['candidates'][0]['personId']
        try :
            userName = studentMap.find_one({'PersonId':Id})['_id']
            listUser.append(userName)
        except:



print()