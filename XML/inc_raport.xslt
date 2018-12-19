<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml"
                              xmlns:lu="http://example.com/lookup" xmlns:f="http://example.com/functions" >

  <xsl:output method="xml" encoding="utf-8" indent="yes"/>

  <xsl:key name="companyById" match="company" use="@id"/>
  <xsl:key name="incidentByCategory" match="incident" use="category/mainCategory" />

  <xsl:variable name='matcher-node' select="document('')/xsl:stylesheet/lu:matcher/lu:priorities"/>

  <xsl:template match="/root">
    <root>
      <xsl:apply-templates select="incidents"/>
      <stats>
        <numberOfIncidents>
          <xsl:value-of select="count(/root/incidents/incident)"/>
        </numberOfIncidents>
        <mostCommonCategory>
          <xsl:for-each select="/root/incidents/incident/category/mainCategory">
            <xsl:sort select="count(key('incidentByCategory', .))" data-type="number" order="descending" />
            <xsl:if test="position() = 1">
              <xsl:value-of select="." />
            </xsl:if>
          </xsl:for-each>       
        </mostCommonCategory>
        <reportDate>
          <xsl:value-of  select="f:currentDateTime()"/>
        </reportDate>
      </stats>
    </root>
  </xsl:template>

  <xsl:template match="incidents">
    <incidents>
      <xsl:apply-templates select="incident">
        <xsl:sort select="state"/>
      </xsl:apply-templates>
    </incidents>
  </xsl:template>

  <xsl:template match="incident">
    <xsl:variable name='curr_impact' select='priority/impact'/>
    <xsl:variable name='curr_urgency' select='priority/urgency'/>
    <incident>
      <incidentID>
        <xsl:value-of select="incidentID" />
      </incidentID>
      <endUser>
        <xsl:value-of select="endUser" />
      </endUser>
      <dateOpened>
        <xsl:value-of select="dateOpened" />
      </dateOpened>
      <affectedUsers>
        <xsl:value-of select="affectedUsers" />
      </affectedUsers>
      <state>
        <xsl:value-of select="state" />
      </state>
      <category>
        <mainCategory>
          <xsl:value-of select="category/mainCategory" />
        </mainCategory>
        <subcategory>
          <xsl:value-of select="category/subcategory" />
        </subcategory>
      </category>
      <priority>
        <xsl:value-of select="$matcher-node[@impact=$curr_impact and @urgency=$curr_urgency]/@priority"/>
      </priority>
      <assignmentGroup>
        <xsl:value-of select="assignmentGroup" />
      </assignmentGroup>
      <phoneNumber>
        <xsl:value-of select="phoneNumber" />
      </phoneNumber>
      <userCompany>
        <xsl:value-of select="key('companyById', userCompany/@comid)" />
      </userCompany>
      <shortDescription>
       <xsl:value-of select="shortDescription" />
      </shortDescription>
      <description>
        <xsl:value-of select="description" />
      </description>
      <workNotes>
        <xsl:value-of select="workNotes" />
      </workNotes>
    </incident>
  </xsl:template>

  <lu:matcher>
    <lu:priorities impact="Low" urgency="1" priority="P3"/>
    <lu:priorities impact="Low" urgency="2" priority="P3"/>
    <lu:priorities impact="Low" urgency="3" priority="P4"/>
    <lu:priorities impact="Low" urgency="4" priority="P4"/>

    <lu:priorities impact="Medium" urgency="1" priority="P1"/>
    <lu:priorities impact="Medium" urgency="2" priority="P2"/>
    <lu:priorities impact="Medium" urgency="3" priority="P3"/>
    <lu:priorities impact="Medium" urgency="4" priority="P4"/>

    <lu:priorities impact="High" urgency="1" priority="P1"/>
    <lu:priorities impact="High" urgency="2" priority="P1"/>
    <lu:priorities impact="High" urgency="3" priority="P2"/>
    <lu:priorities impact="High" urgency="4" priority="P3"/>
  </lu:matcher>

</xsl:stylesheet>
