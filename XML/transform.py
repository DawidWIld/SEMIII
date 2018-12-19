import lxml.etree as ET
import datetime

def currentDateTime(context, arg=None):
    return str(datetime.datetime.now())

ns = ET.FunctionNamespace('http://example.com/functions')
ns['currentDateTime'] = currentDateTime

dom = ET.parse("incidents.xml")
xslt = ET.parse("inc_raport.xslt")
transform = ET.XSLT(xslt)
newdom = transform(dom)
open("output.xml", "w").write(ET.tostring(newdom, pretty_print=True).decode("utf-8"))
#print(ET.tostring(newdom, pretty_print=True).decode("utf-8"))
print(transform.error_log)