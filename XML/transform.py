import lxml.etree as ET
import datetime

def currentDateTime(context, arg=None):
    return str(datetime.datetime.now())

def center(context, arg=None):
    pass

ns = ET.FunctionNamespace('http://example.com/functions')
ns['currentDateTime'] = currentDateTime
ns['center'] = center

def do_xslt(a, b, c):
    dom = ET.parse(a)
    xslt = ET.parse(b)
    transform = ET.XSLT(xslt)
    newdom = transform(dom)
    #open(c, "w").write(ET.tostring(newdom, pretty_print=True).decode("utf-8"))
    open(c, "w").write(str(newdom))
    #print(str(newdom))
    #print(ET.tostring(newdom, pretty_print=True).decode("utf-8"))
    print(transform.error_log)


do_xslt("incidents.xml", "inc_raport.xslt", "output.xml") # stage 1
do_xslt("output.xml", "stage3.xslt", "output3.xml") # stage 3
