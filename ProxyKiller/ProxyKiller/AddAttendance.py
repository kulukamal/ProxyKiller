import cognitive_face as CF
import requests
from io import BytesIO
from PIL import Image, ImageDraw
import glob
import pymongo
def Min(a,b):
    if(a<b):
        return a
    else:
        return b

KEY = '19cbaf8a3a224dd687d5fe31f6ef353b'  # Replace with a valid subscription key (keeping the quotes in place).
CF.Key.set(KEY)
PERSON_GROUP_ID = 'proxykiller'
BASE_URL = 'https://centralindia.api.cognitive.microsoft.com/face/v1.0'  # Replace with your regional Base URL
CF.BaseUrl.set(BASE_URL)



client = pymongo.MongoClient()
db = client['ProxyKiller']
buffer = db['buffer']
iBuffer = db['imageBuffer']
studentMap = db['studentMap']
attendanceMap = db['attendanceMap']

subjectId = buffer.find_one({'_id':{'$ne' : '1-1'}})['_id']
imgPath = iBuffer.find_one({'_id':{'$ne' : '1-1'}})['_id']
subject = db[str(subjectId)]
'''subjects = subject.find({})
for x in subjects:
    print(x)'''


faces = CF.face.detect(imgPath)
face_ids = [d['faceId'] for d in faces]



i = 0
Id = 0
end = len(face_ids)-1
flag = 0
while i <= end :
    identified_faces = CF.face.identify(face_ids[i:Min(i+9,end)+1], PERSON_GROUP_ID)
    i = i + 10
    for face in identified_faces:
        if(len(face['candidates'])!=0):
            Id = face['candidates'][0]['personId']
            print(face)
            try :
                userName = studentMap.find_one({'PersonId':Id})['_id']
                print(userName + " : " +  bstr(face['candidates'][0]['confidence']))
                myDict = {'_id':userName}
                up = {'$set':{'Buffer':1}}
                attendanceMap.update_one(myDict,up)
                flag = 1
            except:
                print('failed')
if(flag):
    buffer.delete_one({'_id':subjectId})
    iBuffer.delete_one({'_id':imgPath})
    print("updated")

