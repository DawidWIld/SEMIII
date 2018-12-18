import lxml.etree as ET

dom = ET.parse("incidents.xml")
xslt = ET.parse("inc_raport.xslt")
transform = ET.XSLT(xslt)
newdom = transform(dom)
open("output.xml", "w").write(ET.tostring(newdom, pretty_print=True).decode("utf-8"))