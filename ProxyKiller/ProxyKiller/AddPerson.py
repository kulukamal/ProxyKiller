import cognitive_face as CF
import requests
from io import BytesIO
from PIL import Image, ImageDraw
import glob
import pymongo

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
studentPicture = db['studentPicture']

userName = buffer.find_one({'_id':{'$ne' : '-1'}})['_id']
buffer.delete_one({'_id':userName})
print(userName)
pictures = studentPicture.find_one({'_id':userName})

Id = CF.person.create(PERSON_GROUP_ID,userName)['personId']
print(Id)
try:
    for path in pictures['ImageLocations']:
        CF.person.add_face(path,PERSON_GROUP_ID,Id)
    CF.person_group.train(PERSON_GROUP_ID)
    myDict = {'_id':userName,'PersonId':Id}
    studentMap.insert_one(myDict)
    print("success")
except:
    print("fail")
    CF.person.delete(PERSON_GROUP_ID,Id)
